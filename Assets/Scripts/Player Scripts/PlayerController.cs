using Core;
using UnityEngine;

namespace Player_Scripts
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private bool isRunner = false;
        //checkers
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private Transform groundChecker;
        //Buttons
        [SerializeField] private PressChecker rightButton;
        [SerializeField] private PressChecker leftButton;

        [HideInInspector] public char xOrY;
        [SerializeField] private float maxSpeed;
        [SerializeField] private int facingDirection = 1;

        public int FacingDirection => facingDirection;

        //--------------------------Stats--------------------------
        private float _accelerationRate;
        private float _jumpHeight;
        private float _secondJumpHeight;
        private bool _canWallClimb = false;
        private bool _secondJumpAllowed;
        private bool _canJumpMore;

        //-------------------------------------------------------
        private float _buttonSmooth = 0.0f;
        private float _keyboardSmooth = 0.0f;
        private float _groundCheckRadius = 0.05f;
        private Rigidbody2D _myRB;
        private Animator _playerXAnim;
        private Animator _playerYAnim;
        private CapsuleCollider2D _playerBody;
        private GameObject _attackArea;
        private Transform _playerSize;
        private bool _facingRight;
        private bool _isGrounded;
        private bool _keyboardCheck = true;
        private bool _buttonCheck = true;

        private const string AttackArea = "AttackArea";

        private static readonly int Grounded = Animator.StringToHash("isGrounded");
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int VerticalSpeed = Animator.StringToHash("verticalSpeed");

        void Start()
        {
            _myRB = GetComponent<Rigidbody2D>();
            _playerXAnim = gameObject.transform.GetChild(0).GetComponent<Animator>();
            _playerYAnim = gameObject.transform.GetChild(1).GetComponent<Animator>();
            _playerBody = gameObject.GetComponent<CapsuleCollider2D>();
            _attackArea = GameObject.Find(AttackArea);
            _playerSize = GetComponentInChildren<Transform>();
            _facingRight = true;
        }

        void Update()
        {
            Jumping(true);
            Slipping();
            Moving(true);
        }

        private void Runner()
        {
            MoveCharacter(1, ref _keyboardSmooth, ref _keyboardCheck);
            CheckingGrounded(_playerXAnim.gameObject.activeSelf ? _playerXAnim : _playerYAnim);
        }

        public void UpdateStats()
        {
            Stats stats = GetComponentInChildren<Stats>();
            maxSpeed = stats.maxSpeed;
            _accelerationRate = stats.acceleration;
            _jumpHeight = stats.jumpHeight;
            _secondJumpHeight = stats.secondJumpHeight;
            _canWallClimb = stats.canWallClimb;
            xOrY = stats.xOrY;
        }

        public void Moving(bool isPC)
        {
            Animator characterAnim = _playerXAnim.gameObject.activeSelf ? _playerXAnim : _playerYAnim;

            CheckingGrounded(characterAnim);

            CheckingPressedButtons(isPC, characterAnim);

            // Flipping character
            if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || rightButton.isPressed) &&
                !_facingRight) flip();
            if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || leftButton.isPressed) &&
                _facingRight) flip();
        }

        private void CheckingGrounded(Animator characterAnim)
        {
            _isGrounded = Physics2D.OverlapCircle(groundChecker.position, _groundCheckRadius, groundLayer);
            characterAnim.SetBool(Grounded, _isGrounded);
            // characterAnim.SetFloat("verticalSpeed", myRB.velocity.y);
        }

        private void CheckingPressedButtons(bool isPC, Animator characterAnim)
        {
            characterAnim.SetFloat(Speed, Mathf.Abs(_keyboardSmooth));

            // Pressing left keyboard buttons

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                MoveCharacter(-1, ref _keyboardSmooth, ref _keyboardCheck);
            }

            // Pressing right keyboard buttons

            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                MoveCharacter(1, ref _keyboardSmooth, ref _keyboardCheck);
            }
            // NOT pressing left and right keyboard buttons

            else if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.A) &&
                     !Input.GetKey(KeyCode.LeftArrow))
            {
                _keyboardCheck = true; // Enabling relevant condition

                _keyboardSmooth -= _accelerationRate;

                if (_keyboardSmooth <= 0) _keyboardSmooth = 0;
            }

            characterAnim.SetFloat(Speed, Mathf.Abs(_buttonSmooth));
            // Button Moving * Button Moving * Button Moving * Button Moving * Button Moving *

            if (leftButton.isPressed)
            {
                MoveCharacter(-1, ref _buttonSmooth, ref _buttonCheck);
            }

            else if (rightButton.isPressed)
            {
                MoveCharacter(1, ref _buttonSmooth, ref _buttonCheck);
            }

            else if (!rightButton.isPressed && !leftButton.isPressed)
            {
                _buttonCheck = true; // Enabling relevant condition

                _buttonSmooth -= _accelerationRate;

                if (_buttonSmooth <= 0) _buttonSmooth = 0;
            }
        }

        private void MoveCharacter(sbyte leftOrRight, ref float smoothing, ref bool checking)
        {
            if (smoothing > 0 && checking) // If player press button before keyboardSmooth reached zero
            {
                smoothing = 0;
                checking = false;
            }

            smoothing += _accelerationRate;
            if (smoothing >= 1) smoothing = 1;
            _myRB.linearVelocity = new Vector2(leftOrRight * maxSpeed * smoothing, _myRB.linearVelocity.y);

            //--------------------------------Скольжение вниз по стенке------------------------------
        }

        public void Jumping(bool isPC) // Character jumping
        {
            Animator characterAnim;

            characterAnim = _playerXAnim.gameObject.activeSelf ? _playerXAnim : _playerYAnim;

            secondJumping(isPC);
            if (isPC && _isGrounded && Input.GetKey(KeyCode.Space))
            {
                _isGrounded = false;
                characterAnim.SetBool(Grounded, _isGrounded);
                _myRB.linearVelocity = new Vector2(_myRB.linearVelocity.x, _jumpHeight);
                characterAnim.SetFloat(VerticalSpeed, _myRB.linearVelocity.y);
            }

            else if (!isPC && _isGrounded) // проверяю это через комп или нет и в компоненте кнопки вызываю эту функцию
            {
                _isGrounded = false;
                characterAnim.SetBool(Grounded, _isGrounded);
                _myRB.linearVelocity = new Vector2(_myRB.linearVelocity.x, _jumpHeight);
            }
        }

        private void secondJumping(bool isPC)
        {
            if (_isGrounded) _canJumpMore = true;

            if (!_isGrounded) _secondJumpAllowed = true;

            if (_canJumpMore && _secondJumpAllowed)
            {
                if (isPC && Input.GetKeyDown(KeyCode.Space))
                {
                    _myRB.linearVelocity = new Vector2(_myRB.linearVelocity.x, _secondJumpHeight);
                    _canJumpMore = false;
                }
                else if (!isPC)
                {
                    _myRB.linearVelocity = new Vector2(_myRB.linearVelocity.x, _secondJumpHeight);
                    _canJumpMore = false;
                }
            }
        }

        private void flip()
        {
            facingDirection *= -1;
            _facingRight = !_facingRight;

            Vector3 Scale = transform.localScale;
            Scale.x *= -1;
            transform.localScale = Scale;
        }

        private void Slipping()
        {
            if (Input.GetKey(KeyCode.S))
            {
                _playerBody.offset = new Vector2(0.0f, -0.25f);
                _playerBody.size = new Vector2(2f, 1.0f);
                _playerBody.direction = CapsuleDirection2D.Horizontal;

                // AttackArea.SetActive(false);

                // if(facingRight) playerSize.localScale = new Vector3(1.5f, 0.75f, 0.0f);
                // if(!facingRight) playerSize.localScale = new Vector3(-1.5f, 0.75f, 0.0f);
            }
            else if (!Input.GetKey(KeyCode.S))
            {
                _playerBody.offset = new Vector2(0.0f, -0.2f);
                _playerBody.size = new Vector2(1.0f, 2.5f);
                _playerBody.direction = CapsuleDirection2D.Vertical;

                // AttackArea.SetActive(true);

                // if(facingRight) playerSize.localScale = new Vector3(1.0f, 1.0f, 0.0f);
                // if(!facingRight) playerSize.localScale = new Vector3(-1.0f, 1.0f, 0.0f);
            }
        }
    }
}
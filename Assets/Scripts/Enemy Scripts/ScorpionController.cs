using Common;
using Core;
using UnityEngine;

namespace Enemy_Scripts
{
    public class ScorpionController : MonoBehaviour
    {
        [SerializeField] float normalSpeed;
        [SerializeField] float scorpionWalkingArea;

        bool _facingRight = false;

        Vector3 _pointOfOrigin;
        PlayerDetectionScript _playerDetection;
        GameObject _player;
        Health _playerHealth;
        Animator _myAnim;
        Rigidbody2D _scorpionRB;

        void Start()
        {
            _playerDetection = GetComponentInChildren<PlayerDetectionScript>();
            _scorpionRB = GetComponent<Rigidbody2D>();
            _myAnim = GetComponent<Animator>();
            _pointOfOrigin = transform.position;
            _player = GameObject.FindWithTag(Tags.PlayerTag);
        }

        void Update()
        {
            if (_facingRight) _scorpionRB.linearVelocity = new Vector2(normalSpeed, _scorpionRB.linearVelocity.y);
            if (!_facingRight) _scorpionRB.linearVelocity = new Vector2(-normalSpeed, _scorpionRB.linearVelocity.y);

            if (transform.position.x > _pointOfOrigin.x + scorpionWalkingArea)
            {
                flip();
                _pointOfOrigin.x += 1;
            }

            if (transform.position.x < _pointOfOrigin.x - scorpionWalkingArea)
            {
                flip();
                _pointOfOrigin.x -= 1;
            }

            if (_playerDetection.PlayerDetected)
            {
                _myAnim.SetBool("isRunning", true);
                if (_player.transform.position.x < transform.position.x)
                {
                    if (_facingRight) flip();
                    _scorpionRB.linearVelocity = new Vector2(-3 * normalSpeed, _scorpionRB.linearVelocity.y);
                }

                if (_player.transform.position.x > transform.position.x)
                {
                    if (!_facingRight) flip();
                    _scorpionRB.linearVelocity = new Vector2(3 * normalSpeed, _scorpionRB.linearVelocity.y);
                }
            }
        }

        private void flip()
        {
            _facingRight = !_facingRight;
            Vector3 Scale = transform.localScale;
            Scale.x *= -1;
            transform.localScale = Scale;
        }
    }
}
using Common;
using Core;
using UnityEngine;

namespace Enemy_Scripts
{
    public class FlyingEnemyScript : MonoBehaviour
    {
        [SerializeField] float _speed;
        [SerializeField] float _shootRate;
        [SerializeField] float _distanceFromPlayer;
        [SerializeField] GameObject missile;
        [SerializeField] Transform missileStartPoint;

        private Transform _player;
        private Animator _dragonAnim;
        private Health _dragonHealth;
        private float _shootTime = 0.0f;
        private bool _isPlayerDetected = false;
        private bool _isPlayerInZone = false;
        private bool _facingRight = true;

        private static readonly int DragonDead = Animator.StringToHash("dragonDead");

        void Start()
        {
            _player = GameObject.FindWithTag(Tags.PlayerTag).GetComponent<Transform>();
            _dragonAnim = GetComponent<Animator>();
            _dragonHealth = GetComponentInChildren<Health>();
        }

        void Update()
        {
            if (_isPlayerDetected && Vector2.Distance(transform.position, _player.position) > _distanceFromPlayer)
            {
                transform.position = Vector2.MoveTowards(transform.position, _player.position, _speed * Time.deltaTime);
            }


            if (_isPlayerInZone && Time.time > _shootTime)
            {
                if (_facingRight)
                    Instantiate(missile, missileStartPoint.position, Quaternion.AngleAxis(-180, Vector3.right));
                if (!_facingRight)
                    Instantiate(missile, missileStartPoint.position, Quaternion.AngleAxis(180, Vector3.up));
                _shootTime = Time.time + _shootRate;
            }

            if (transform.position.x > _player.position.x && _facingRight) Flip();
            if (transform.position.x < _player.position.x && !_facingRight) Flip();

            if (_dragonHealth.CurrentHealth <= 0)
            {
                _dragonAnim.SetBool(DragonDead, true);
                Destroy(gameObject, 0.8f);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(Tags.PlayerTag))
            {
                _isPlayerDetected = true;
            }
        }

        void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag(Tags.PlayerTag))
            {
                _isPlayerInZone = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(Tags.PlayerTag))
            {
                _isPlayerInZone = false;
            }
        }

        private void Flip()
        {
            _facingRight = !_facingRight;
            Vector3 Scale = transform.localScale;
            Scale.x *= -1;
            transform.localScale = Scale;
        }
    }
}
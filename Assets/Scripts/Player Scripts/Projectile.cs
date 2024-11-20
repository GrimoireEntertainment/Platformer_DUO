using Core;
using UnityEngine;

namespace Player_Scripts
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 30;

        private PlayerController _playerController;
        private PlayerAttackAreaScript _playerAttack;
        private Rigidbody2D _rb;
        private float _nextRangedDamage;
        private float _nextRangedDamageRate = 0.5f;

        private void Start()
        {
            _playerController = GameObject.Find("PlayerContainer").GetComponent<PlayerController>();
            _playerAttack = GameObject.Find("Player_Y").GetComponentInChildren<PlayerAttackAreaScript>();
            _rb = GetComponent<Rigidbody2D>();

            _rb.AddForce(new Vector2(_playerController.FacingDirection * speed, 0), ForceMode2D.Impulse);

            Destroy(gameObject, 2);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy") && Time.time > _nextRangedDamage)
            {
                _nextRangedDamage = Time.time + _nextRangedDamageRate;
                Health enemyHealth = other.gameObject.GetComponent<Health>();
                enemyHealth.AddDamage(_playerAttack.PlayerDamage);
                Destroy(gameObject);
            }
        }
    }
}
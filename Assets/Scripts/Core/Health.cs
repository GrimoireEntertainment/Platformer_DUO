using Common;
using UnityEngine;

namespace Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float _currentHealth;

        public float CurrentHealth => _currentHealth;

        public void AddDamage(float damage)
        {
            _currentHealth -= damage;
            if (_currentHealth - damage < 0) _currentHealth = 0;

            if (!transform.CompareTag(Tags.PlayerTag) && !transform.CompareTag(Tags.FlyingDragon) &&
                _currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
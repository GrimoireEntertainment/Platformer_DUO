using Common;
using Core;
using UnityEngine;

namespace Enemy_Scripts
{
    public class EnemyDamage : MonoBehaviour
    {
        [SerializeField] float enemyDamage;
        [SerializeField] float damageRate;

        private float _nextDamage = 0.0f;

        void OnTriggerStay2D(Collider2D other)
        {
            if (!CompareTag(Tags.PlayerTag)) return;

            if (Time.time > _nextDamage)
            {
                Health playerHealth = other.gameObject.GetComponent<Health>();
                playerHealth.AddDamage(enemyDamage);
                _nextDamage = Time.time + damageRate;
            }
        }
    }
}
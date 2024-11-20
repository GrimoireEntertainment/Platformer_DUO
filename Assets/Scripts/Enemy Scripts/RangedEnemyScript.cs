using Common;
using UnityEngine;

namespace Enemy_Scripts
{
    public class RangedEnemyScript : MonoBehaviour
    {
        [SerializeField] bool _cactusType;

        [SerializeField] Transform shootingStartPoint;
        [SerializeField] GameObject enemyWeapon;
        [SerializeField] float shootRate;

        private float _shootTime;
        private bool _isPlayerHere;

        void Update()
        {
            // Первый тип - постоянно стреляющий через shootRate секунд
            if (_cactusType)
            {
                RangedShooting();
            }

            // Второй тип - появляется и стреляет тогда, когда игрок заходит в определенную зону
            if (!_cactusType && _isPlayerHere)
            {
                RangedShooting();
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(Tags.PlayerTag))
            {
                _isPlayerHere = true;
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(Tags.PlayerTag))
            {
                _isPlayerHere = false;
            }
        }

        private void RangedShooting()
        {
            if (Time.time > _shootTime)
            {
                Instantiate(enemyWeapon, shootingStartPoint.position, transform.rotation);
                _shootTime = Time.time + shootRate;
            }
        }
    }
}
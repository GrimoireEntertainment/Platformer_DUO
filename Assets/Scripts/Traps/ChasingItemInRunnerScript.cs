using Common;
using Core;
using Player_Scripts;
using UnityEngine;

namespace Traps
{
    public class ChasingItemInRunnerScript : MonoBehaviour
    {
        [SerializeField] GameObject playerX;
        [SerializeField] GameObject playerY;

        private float _playerXSpeed;
        private float _playerYSpeed;

        void Start()
        {
            _playerXSpeed = playerX.GetComponent<Stats>().maxSpeed; // Берем скорости двух персонажей по отдельности
            _playerYSpeed = playerY.GetComponent<Stats>().maxSpeed;
        }

        void FixedUpdate()
        {
            if (playerX.activeSelf) // Проверяем какой из персонажей сейчас активен
            {
                transform.Translate(_playerXSpeed / 1.11f * Time.deltaTime, 0, 0);
            }
            else
            {
                transform.Translate(_playerYSpeed / 1.12f * Time.deltaTime, 0, 0);
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(Tags.PlayerTag))
            {
                other.GetComponent<Health>().AddDamage(1000);
            }
        }
    }
}
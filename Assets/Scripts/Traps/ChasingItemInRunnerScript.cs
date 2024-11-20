using Core;
using Player_Scripts;
using UnityEngine;

namespace Traps
{
    public class ChasingItemInRunnerScript : MonoBehaviour
    {
        [SerializeField] GameObject playerX;
        [SerializeField] GameObject playerY;
        private float playerXSpeed;
        private float playerYSpeed;

        // Start is called before the first frame update
        void Start()
        {
            playerXSpeed = playerX.GetComponent<Stats>().maxSpeed; // Берем скорости двух персонажей по отдельности
            playerYSpeed = playerY.GetComponent<Stats>().maxSpeed;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if(playerX.activeSelf) // Проверяем какой из персонажей сейчас активен
            {
                transform.Translate(playerXSpeed/1.11f * Time.deltaTime, 0, 0);
            }
            else
            {
                transform.Translate(playerYSpeed/1.12f * Time.deltaTime, 0, 0);
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if(other.tag == "Player")
            {
                other.GetComponent<Health>().currentHealth = 0;
            }
        }
    }
}

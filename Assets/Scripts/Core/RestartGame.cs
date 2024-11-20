using Common;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class RestartGame : MonoBehaviour
    {
        [SerializeField] string restartSceneName;
        [SerializeField] float restartTime;

        private bool _gameRestart;
        private float _pointOfTime;

        void Update()
        {
            // Если здоровье игрока ниже или равен нулю, завершаем игру
            // Выражение !gameRestart позволяет использовать блок if лишь один раз
            // тем самым предотвращает бесконечный цикл со сдвигом переменной pointOfTime
            if (GameObject.FindWithTag(Tags.PlayerTag).GetComponent<Health>().CurrentHealth <= 0 && !_gameRestart)
            {
                GameOver();
            }

            // Загружаем необходимую сцену
            if (Time.time > _pointOfTime && _gameRestart)
            {
                SceneManager.LoadScene(restartSceneName);
                _gameRestart = false;
            }
        }

        // Нужен на случай, если игрок упал в платформы
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(Tags.PlayerTag))
            {
                GameOver();
            }
        }

        // Функция, извещающая о том, что игра завершена
        private void GameOver()
        {
            _gameRestart = true;
            _pointOfTime = Time.time + restartTime;
        }
    }
}
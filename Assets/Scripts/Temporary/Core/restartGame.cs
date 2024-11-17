using UnityEngine;
using UnityEngine.SceneManagement;

public class restartGame : MonoBehaviour
{
    private float pointOfTime;
    [SerializeField] string restartSceneName;
    [SerializeField] float restartTime;
    private bool gameRestart;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Если здоровье игрока ниже или равен нулю, завершаем игру
        // Выражение !gameRestart позволяет использовать блок if лишь один раз
        // тем самым предотвращает бесконечный цикл со сдвигом переменной pointOfTime
        if(GameObject.FindWithTag("Player").GetComponent<Health>()._currentHealth <= 0 && !gameRestart) 
        {
            GameOver();
        }

        // Загружаем необходимую сцену
        if(Time.time > pointOfTime && gameRestart) 
        {
            SceneManager.LoadScene(restartSceneName);
            gameRestart = false;
        }
    }

    // Нужен на случай, если игрок упал в платформы
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player") {
            GameOver();
        }
    }
    // Функция, извещающая о том, что игра завершена
    private void GameOver() 
    {
        gameRestart = true;
        pointOfTime = Time.time + restartTime;
    }
}

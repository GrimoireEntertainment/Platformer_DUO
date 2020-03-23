using UnityEngine;
using UnityEngine.SceneManagement;

public class restartGame : MonoBehaviour
{
    [SerializeField] float restartTime;
    float startTime;
    bool gameRes = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        startTime += Time.deltaTime;
        if(startTime > restartTime && gameRes) {
            SceneManager.LoadScene("CharMovement", LoadSceneMode.Single);
            gameRes = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player") {
            gameRes = true;
            startTime = 0.0f;
        }
    }
}

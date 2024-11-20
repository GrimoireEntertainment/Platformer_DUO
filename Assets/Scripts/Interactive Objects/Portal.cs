using UnityEngine;
using UnityEngine.SceneManagement;

namespace Interactive_Objects
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] string nextSceneName = null;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                SceneManager.LoadScene(nextSceneName);
            }
        }
    }
}
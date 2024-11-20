using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class SceneLoading : MonoBehaviour
    {
        [SerializeField] string loadScene = "";

        private const string PauseSceneLoader = "PauseSceneLoader";
        private const string ResumeSceneLoader = "ResumeSceneLoader";
        private const string RestartSceneLoader = "RestartSceneLoader";

        private string _prevScene;

        public void OnMouseDown()
        {
            if (gameObject.name == PauseSceneLoader) _prevScene = SceneManager.GetActiveScene().name;

            if (gameObject.name == ResumeSceneLoader || gameObject.name == RestartSceneLoader) loadScene = _prevScene;

            SceneManager.LoadScene(loadScene);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoading : MonoBehaviour
{
    [SerializeField] string loadScene = "";
    private static string prevScene;
    public void OnMouseDown()
    {
        if(gameObject.name == "PauseSceneLoader") prevScene = SceneManager.GetActiveScene().name;
        if(gameObject.name == "ResumeSceneLoader" || gameObject.name == "RestartSceneLoader") loadScene = prevScene;

        SceneManager.LoadScene(loadScene);
    }

}

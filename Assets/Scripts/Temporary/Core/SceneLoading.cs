using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoading : MonoBehaviour
{
    [SerializeField] string loadScene = "";
    public void OnMouseDown()
    {
        SceneManager.LoadScene(loadScene);
    }
}

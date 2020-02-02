using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string buildIndex;


    public void LoadScenes()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(buildIndex);

    }



    public void QuitGame()
    {
        Application.Quit();

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIManager : MonoBehaviour
{
    public string nextScene;
    public GameObject authorsPanel;

    public void LoadGame()
    {
        SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
    }

    public void Authors()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

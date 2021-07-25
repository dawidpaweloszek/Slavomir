using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIManager : MonoBehaviour
{
    public string nextScene;
    public GameObject menuPanel;
    public GameObject authorsPanel;

    private void Start()
    {
        SaveAndLoadSystem.SaveData(0, 0);
        menuPanel.SetActive(true);
        authorsPanel.SetActive(false);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
    }

    public void Authors()
    {
        authorsPanel.SetActive(true);
        menuPanel.SetActive(false);
    }

    public void Back()
    {
        authorsPanel.SetActive(false);
        menuPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

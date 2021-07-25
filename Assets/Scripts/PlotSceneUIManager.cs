using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlotSceneUIManager : MonoBehaviour
{
    public string nextScene;
    public GameObject[] scenes;
    public int sceneIndex = 0;

    // Update is called once per frame
    void Update()
    {
        if (sceneIndex < scenes.Length)
        {
            for (int i = 0; i < scenes.Length; i++)
            {
                scenes[i].SetActive(false);

                if (i == sceneIndex)
                {
                    scenes[i].SetActive(true);
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                sceneIndex++;   
            }
        }
        else
        {
            SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
        }
    }
}

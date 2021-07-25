using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GamesEnds : MonoBehaviour
{
    public bool isSpaceBeenPressed;
    public string sceneToLoad;
    public bool verdict;
    public GameObject winner;
    public GameObject losser;
    public Points points;
    public int pointsToAdd;

    // Update is called once per frame
    void Update()
    {
        if (verdict)
        {
            losser.SetActive(false);
            winner.SetActive(true);

            points.points = pointsToAdd;
        }
        else
        {
            winner.SetActive(false);
            losser.SetActive(true);
        }


        if (!isSpaceBeenPressed)
        {
            Time.timeScale = 0;
            if (Input.GetKey(KeyCode.Space))
            {
                isSpaceBeenPressed = true;
            }
        }

        if (isSpaceBeenPressed)
        {
            Time.timeScale = 1;

            gameObject.SetActive(false);
            
            SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
        }
    }
}

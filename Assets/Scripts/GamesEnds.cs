using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GamesEnds : MonoBehaviour
{
    public bool isSpaceBeenPressed;
    public string sceneToLoad;
    public string verdict;
    public TMP_Text verdictText;

    // Update is called once per frame
    void Update()
    {
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
        }
    }
}

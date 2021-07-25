using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamesInstruction : MonoBehaviour
{
    public bool isSpaceBeenPressed;

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

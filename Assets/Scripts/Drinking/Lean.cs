using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lean : MonoBehaviour
{
    [SerializeField] private GameObject aKeyImage;
    [SerializeField] private GameObject dKeyImage;
    [SerializeField] private int directionOfLean;
    [SerializeField] private float maxTimeBetweenLeans;
    [SerializeField] private float minTimeBetweenLeans;
    [SerializeField] private float timeBetweenLeans;
    [SerializeField] private float cTimeBetweenLeans;
    [SerializeField] private float timeForReaction;
    [SerializeField] private float cTimeForReaction;

    private void Start()
    {
        RandomizeTimeBetweenLeans();
    }

    // Update is called once per frame
    void Update()
    {
        cTimeBetweenLeans += Time.deltaTime;

        if (cTimeBetweenLeans >= timeBetweenLeans)
        {
            RandomizeDirectionOfLean();

            cTimeForReaction += Time.deltaTime;

            if (cTimeForReaction <= timeForReaction)
            {
                if (directionOfLean == -1)
                {
                    dKeyImage.SetActive(true);

                    if (Input.GetKey(KeyCode.D))
                    {
                        cTimeForReaction = 0;
                        RandomizeTimeBetweenLeans();
                        dKeyImage.SetActive(false);
                    }

                    if (Input.GetKey(KeyCode.A))
                    {
                        Time.timeScale = 0;
                    }
                }
                if (directionOfLean == 1)
                {
                    aKeyImage.SetActive(true);

                    if (Input.GetKey(KeyCode.A))
                    {
                        cTimeForReaction = 0;
                        RandomizeTimeBetweenLeans();
                        aKeyImage.SetActive(false);
                    }

                    if (Input.GetKey(KeyCode.D))
                    {
                        Time.timeScale = 0;
                    }
                }
            }
            else
            {
                Time.timeScale = 0;
            }
        }
    }

    private void RandomizeDirectionOfLean()
    {
        if (cTimeForReaction <= 0)
        {
            int direction = 0;

            while (direction == 0)
            {
                direction = Random.Range(-1, 2);
            }

            directionOfLean = direction;
        }
    }

    private void RandomizeTimeBetweenLeans()
    {
        timeBetweenLeans = Random.Range(minTimeBetweenLeans, maxTimeBetweenLeans);
        cTimeBetweenLeans = 0;
    }
}

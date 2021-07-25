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
    //[SerializeField] private float timeForReaction;
    [SerializeField] private float cTimeForReaction;
    [SerializeField] private float timeOfLeaningAnimation;
    //[SerializeField] private float cTimeOfLeaningAnimation;
    public Animator animator;

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
            animator.SetBool("IsLeaningForward", false);
            animator.SetBool("IsLeaningBack", false);

            RandomizeDirectionOfLean();

            cTimeForReaction += Time.deltaTime;

            if (cTimeForReaction <= timeOfLeaningAnimation)
            {
                if (directionOfLean == -1)
                {
                    animator.SetBool("IsLeaningBack", true);

                    dKeyImage.SetActive(true);

                    if (Input.GetKey(KeyCode.D))
                    {
                        cTimeForReaction = 0;
                        RandomizeTimeBetweenLeans();
                        dKeyImage.SetActive(false);
                    }

                    if (Input.GetKey(KeyCode.A))
                    {
                        animator.SetBool("IsFalling", true);
                    }
                }
                if (directionOfLean == 1)
                {
                    animator.SetBool("IsLeaningForward", true);

                    aKeyImage.SetActive(true);

                    if (Input.GetKey(KeyCode.A))
                    {
                        cTimeForReaction = 0;
                        RandomizeTimeBetweenLeans();
                        aKeyImage.SetActive(false);
                    }

                    if (Input.GetKey(KeyCode.D))
                    {
                        animator.SetBool("IsFalling", true);
                    }
                }
            }
            else
            {
                animator.SetBool("IsFalling", true);
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

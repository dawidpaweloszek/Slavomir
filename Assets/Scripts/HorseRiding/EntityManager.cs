using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EntityManager : MonoBehaviour
{
    [Header("Velocity")]
    public float maxVelocity;
    public float velocity;
    public float acceleration;
    public float deceleration;

    [Header("Button Mashing")]
    public float lastMashTime;
    public float currentMashTime;
    public float difference;

    [Header("UI")]
    public bool isThisPlayer;
    public TMP_Text verdictText;

    public Animator playerAnimator;
    public GamesEnds endScreen;

    public bool CheckForReaction()
    {
        int range = Random.Range(0, 100);

        if (range < 50)
            return true;
        else
            return false;
    }

    void Update()
    {
        if (isThisPlayer)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                // Calculate time since last space hit
                currentMashTime = Time.realtimeSinceStartup;
                difference = currentMashTime - lastMashTime;
                lastMashTime = currentMashTime;

                if (velocity <= maxVelocity)
                    velocity += acceleration / difference;
                else
                    velocity = maxVelocity;

                Debug.Log(difference);
            }
            else
            {
                if (velocity > 0)
                    velocity -= deceleration * Time.deltaTime;
                else
                    velocity = 0;
            }
        }
        else
        {
            if (velocity <= maxVelocity)
                velocity += acceleration * Time.deltaTime;
            else
                velocity = maxVelocity;
        }
        

        transform.position += Vector3.right * Time.deltaTime * velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name == "Finish")
        {
            endScreen.gameObject.SetActive(true);
            if (isThisPlayer)
            {
                endScreen.verdict = true;
            }
            else
            {
                endScreen.verdict = false;
            }

            collision.gameObject.SetActive(false);
        }
    }
}

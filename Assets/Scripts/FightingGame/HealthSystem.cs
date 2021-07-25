using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public GameObject playerFull;
    public GameObject playerHalf;
    public GameObject enemyFull;
    public GameObject enemyHalf;

    public FigthingEntity player;
    public FigthingEntity enemy;

    // Update is called once per frame
    void Update()
    {
        if (player.health == 2)
        {
            playerFull.SetActive(true);
            playerHalf.SetActive(false);
        }
        else if (player.health == 1)
        {
            playerFull.SetActive(false);
            playerHalf.SetActive(true);
        }
        else
        {
            playerFull.SetActive(false);
            playerHalf.SetActive(false);
        }


        if (enemy.health == 2)
        {
            enemyFull.SetActive(true);
            enemyHalf.SetActive(false);
        }
        else    if (enemy.health == 1)
        {
            enemyFull.SetActive(false);
            enemyHalf.SetActive(true);
        }
        else
        {
            enemyFull.SetActive(false);
            enemyHalf.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toss : MonoBehaviour
{
    [SerializeField] private GameObject dice;
    [SerializeField] private Transform hand;
    [SerializeField] private List<GameObject> spawnedDices = new List<GameObject>();
    public Animator animator;
    public float timeToSpawnDices = 3.292f;
    public float cTimeToSpawnDices;


    public int points;
    public bool doPlayerTossed = false;

    public bool wantToToss = false;
    public bool isTossing = false;

    public void TossDices()
    {
        isTossing = true;

        if (wantToToss)
        {
            animator.SetBool("IsTossing", false);
            cTimeToSpawnDices = 0;

            ClearTable();

            for (int i = 0; i < 5; i++)
            {
                var newDice = Instantiate(dice, hand.position, Quaternion.identity);
                newDice.transform.parent = hand;
                spawnedDices.Add(newDice);
            }

            doPlayerTossed = true;
        }
    }

    public void ClearTable()
    {
        for (int i = 0; i < spawnedDices.Count; i++)
        {
            Destroy(spawnedDices[i].gameObject);
        }

        spawnedDices.Clear();
    }

    private void Update()
    {
        if (isTossing && !wantToToss)
        {
            animator.SetBool("IsTossing", true);

            cTimeToSpawnDices += Time.deltaTime;

            if (cTimeToSpawnDices >= timeToSpawnDices)
            {
                wantToToss = true;
                TossDices();
                isTossing = false;
            }
        }

        bool canReadValues = true;

        for (int i = 0; i < spawnedDices.Count; i++)
        {
            if (spawnedDices[i].GetComponent<Dice>().NeedToTossAgain)
            {
                canReadValues = false;
            }
        }

        if (canReadValues)
        {
            ReadValues();
        }
    }

    private void ReadValues()
    {
        int points = 0;
        bool flag = true;

        for (int i = 0; i < spawnedDices.Count; i++)
        {
            var valueName = spawnedDices[i].GetComponent<Dice>().tossedValue;

            if (valueName == "one")
                points += 1;
            if (valueName == "two")
                points += 2;
            if (valueName == "three")
                points += 3;
            if (valueName == "four")
                points += 4;
            if (valueName == "five")
                points += 5;
            if (valueName == "six")
                points += 6;
        }

        this.points = points;
    }
}

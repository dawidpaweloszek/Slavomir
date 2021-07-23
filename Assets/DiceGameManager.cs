using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiceGameManager : MonoBehaviour
{
    [Header("Main values")]
    public TMP_Text verdict;
    public int roundNumber;
    public float readValuesTime;
    public float cReadValuesTime = 4;
    public bool doPlayerWon = false;

    [Header("Player")]
    [SerializeField] private Toss player;
    public TMP_Text playerPointsText;
    public GameObject button;
    public int playerPoints;
    public bool isPlayerTossing = true;
    public bool[] isPlayerWonRound;

    [Header("Enemy")]
    [SerializeField] private Toss enemy;
    public TMP_Text enemyPointsText;
    public int enemyPoints;
    public bool doEnemyTossed;

    void Start()
    {
        ResetRound();
        roundNumber = 0;
    }

    void Update()
    {
        PlayGame();

        // Win before third round
        if (roundNumber == 2)
        {
            // Player won first two
            if (isPlayerWonRound[0] && isPlayerWonRound[1])
            {
                doPlayerWon = true;
            }

            // Enemy won first two
            if (!isPlayerWonRound[0] && !isPlayerWonRound[1])
            {
                doPlayerWon = false;
            }
        }
        // Win after third round
        if (roundNumber == 4)
        {

        }
    }

    private void ResetRound()
    {
        playerPoints = 0;
        enemyPoints = 0;
        isPlayerTossing = true;
        doEnemyTossed = false;
        player.doPlayerTossed = false;
        enemy.doPlayerTossed = false;
        playerPointsText.text = "0";
        enemyPointsText.text = "0";
    }

    private void PlayGame()
    {
        if (isPlayerTossing)
        {
            Debug.Log("player tossing");

            button.SetActive(true);
            if (player.doPlayerTossed)
            {
                verdict.gameObject.SetActive(false);

                button.SetActive(false);

                playerPoints = player.points;

                if (playerPoints != 0)
                {
                    if (cReadValuesTime <= readValuesTime)
                    {
                        cReadValuesTime += Time.deltaTime;
                        playerPointsText.text = playerPoints.ToString();
                    }
                    else
                    {
                        cReadValuesTime = 0;
                        isPlayerTossing = false;
                        player.ClearTable();
                    }
                }    
            }
        }
        else 
        {
            Debug.Log("enemy tossing");

            if (!enemy.doPlayerTossed)
            {
                enemy.TossDices();
            }
            else
            {
                enemyPoints = enemy.points;

                if (enemyPoints != 0)
                {
                    if (cReadValuesTime <= readValuesTime)
                    {
                        enemyPointsText.text = enemyPoints.ToString();
                        cReadValuesTime += Time.deltaTime;
                    }
                    else
                    {
                        cReadValuesTime = 0;
                        doEnemyTossed = true;
                        enemy.ClearTable();
                    }
                }
            }
        }

        if (!isPlayerTossing && doEnemyTossed)
        {
            verdict.gameObject.SetActive(true);

            if (playerPoints > enemyPoints)
            {
                verdict.text = "Gracz wygrywa!";
                isPlayerWonRound[roundNumber] = true;
            }
            else if (playerPoints < enemyPoints)
            {
                verdict.text = "Przeciwnik wygrywa!";
                isPlayerWonRound[roundNumber] = false;
            }
            else
            {
                verdict.text = "Remis!";
                isPlayerWonRound[roundNumber] = false;
            }

            roundNumber++;
            ResetRound();
        }
    }
}

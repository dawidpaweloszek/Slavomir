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
    public GameObject endGameScreen;

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
        if (roundNumber > 1)
        {
            // Player won first two
            if (isPlayerWonRound[0] && isPlayerWonRound[1])
            {
                endGameScreen.SetActive(true);
                endGameScreen.GetComponent<GamesEnds>().verdict = true;
                doPlayerWon = true;
            }

            // Enemy won first two
            if (!isPlayerWonRound[0] && !isPlayerWonRound[1])
            {
                endGameScreen.SetActive(true);
                endGameScreen.GetComponent<GamesEnds>().verdict = false;
                doPlayerWon = false;
            }
        }
        // Win after third round
        if (roundNumber > 2)
        {
            if ((isPlayerWonRound[0] && !isPlayerWonRound[1] && isPlayerWonRound[2]) ||      // 1 0 1
                (!isPlayerWonRound[0] && isPlayerWonRound[1] && isPlayerWonRound[2]))        // 0 1 1
            {
                endGameScreen.SetActive(true);
                endGameScreen.GetComponent<GamesEnds>().verdict = true;
                doPlayerWon = true;
            }
            if ((!isPlayerWonRound[0] && isPlayerWonRound[1] && !isPlayerWonRound[2]) ||      // 0 1 0
                (isPlayerWonRound[0] && !isPlayerWonRound[1] && !isPlayerWonRound[2]))        // 1 0 0
            {
                endGameScreen.SetActive(true);
                endGameScreen.GetComponent<GamesEnds>().verdict = false;
                doPlayerWon = false;
            }
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
        player.isTossing = false;
        player.wantToToss = false;
    }

    private void PlayGame()
    {
        if (isPlayerTossing)
        {
            Debug.Log("player tossing");

            button.SetActive(true);

            if (player.isTossing || player.wantToToss)
            {
                button.SetActive(false);
            }

            if (player.doPlayerTossed)
            {
                verdict.gameObject.SetActive(false);

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
                GameObject.Find("Enemy").GetComponent<Toss>().animator.SetTrigger("IfPlayerWon");
            }
            else if (playerPoints < enemyPoints)
            {
                verdict.text = "Przeciwnik wygrywa!";
                isPlayerWonRound[roundNumber] = false;
                GameObject.Find("Enemy").GetComponent<Toss>().animator.SetTrigger("IfPlayerLost");
            }
            else
            {
                verdict.text = "Remis!";
                isPlayerWonRound[roundNumber] = false;
                GameObject.Find("Enemy").GetComponent<Toss>().animator.SetTrigger("IfPlayerWon");
            }

            roundNumber++;
            ResetRound();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class Targets : MonoBehaviour
{
    public static Targets instance;

    [Header("Targets")]
    [SerializeField] private GameObject[] targets;
    [SerializeField] private TMP_Text targetsText;
    private int targetsCount;
    public int TargetsCount { get => targetsCount; set => targetsCount = value; }

    [Header("Time")]
    [SerializeField] private float timer;
    [SerializeField] private TMP_Text timerText;
    private TimeSpan timePlaying;
    private float elapsedTime;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        timerText.text = "Czas: 00:00.00";
        elapsedTime = 0f;

        targetsCount = targets.Length;
        targetsText.text = "Cele: " + targetsCount.ToString() + "/" + targetsCount.ToString();
    }

    private void Update()
    {
        // Update timer
        elapsedTime += Time.deltaTime;

        timerText.text = timer.ToString();
        timePlaying = TimeSpan.FromSeconds(elapsedTime);
        timerText.text = "Czas: " + timePlaying.ToString("mm':'ss'.'ff");

        // Update targets
        targetsText.text = "Cele: " + targetsCount.ToString() + "/" + targets.Length.ToString();

        if (targetsCount <= 0)
        {
            SceneManager.LoadScene("main_test_scene", LoadSceneMode.Single);
        }
    }
}

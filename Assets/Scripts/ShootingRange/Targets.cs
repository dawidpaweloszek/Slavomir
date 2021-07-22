using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Targets : MonoBehaviour
{
    [Header("Targets")]
    [SerializeField] private GameObject[] targets;
    [SerializeField] private TMP_Text targetsText;
    private int targetsCount;

    [Header("Time")]
    [SerializeField] private float timer;
    [SerializeField] private TMP_Text timerText;
    private TimeSpan timePlaying;
    private float elapsedTime;

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
        targetsText.text = "Cele: " + targets.Length.ToString() + "/" + targetsCount.ToString();
    }
}

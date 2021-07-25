using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Targets : MonoBehaviour
{
    public static Targets instance;

    public GameObject endScreen;

    [Header("Grid")]
    public Vector2 bordersX;
    public Vector2 bordersY;
    public Vector2 bordersZ;
    [SerializeField] private List<Vector3> targetsPosition = new List<Vector3>();

    [Header("Targets")]
    [SerializeField] private int numberOfTargets;
    [SerializeField] private GameObject[] targets;
    [SerializeField] private List<GameObject> spawnedTargets = new List<GameObject>();
    [SerializeField] private TMP_Text targetsText;
    private int targetsCount;
    public int TargetsCount { get => targetsCount; set => targetsCount = value; }

    [Header("Time")]
    [SerializeField] private float timer;
    [SerializeField] private TMP_Text timerText;
    private TimeSpan timePlaying;
    public float elapsedTime;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        CreateGrid();

        for (int i = 0; i < numberOfTargets; i++)
        {
            var newTarget = Instantiate(targets[UnityEngine.Random.Range(0, 3)], new Vector3(targetsPosition[i].x, targetsPosition[i].y, targetsPosition[i].z), Quaternion.identity);
            newTarget.transform.localEulerAngles = new Vector3(90, 0, 0);
            newTarget.transform.parent = GameObject.Find("Targets").transform;

            spawnedTargets.Add(newTarget);
        }

        timerText.text = "Czas: 00:00.00";

        targetsCount = spawnedTargets.Count;
        targetsText.text = "Cele: " + targetsCount.ToString() + "/" + numberOfTargets;
    }

    private void Update()
    {
        // Update timer
        elapsedTime -= Time.deltaTime;

        timerText.text = timer.ToString();
        timePlaying = TimeSpan.FromSeconds(elapsedTime);
        timerText.text = "Czas: " + timePlaying.ToString("mm':'ss'.'ff");

        // Update targets
        targetsText.text = "Cele: " + targetsCount.ToString() + "/" + numberOfTargets;

        if (targetsCount <= 0)
        {
            if (elapsedTime > 0)
            {
                endScreen.SetActive(true);
                endScreen.GetComponent<GamesEnds>().verdict = true;
            }
        }
        if (targetsCount > 0)
        {
            if (elapsedTime <= 0)
            {
                endScreen.SetActive(true);
                endScreen.GetComponent<GamesEnds>().verdict = false;
            }
        }
    }

    private void CreateGrid()
    {
        for (int i = 0; i < numberOfTargets; i++)
        {
            float positionX = UnityEngine.Random.Range(bordersX.x, bordersX.y);
            float positionY = UnityEngine.Random.Range(bordersY.x, bordersY.y);
            float positionZ = UnityEngine.Random.Range(bordersZ.x, bordersZ.y);

            Vector3 position = new Vector3(positionX, positionY, positionZ);

            if (targetsPosition.Count > 0)
            {
                while (Vector3.Distance(targetsPosition[i - 1], position) <= 7f)
                {
                    positionX = UnityEngine.Random.Range(bordersX.x, bordersX.y);
                    positionY = UnityEngine.Random.Range(bordersY.x, bordersY.y);
                    positionZ = UnityEngine.Random.Range(bordersZ.x, bordersZ.y);

                    position = new Vector3(positionX, positionY, positionZ);
                }
            }
            
            targetsPosition.Add(position);
        }
    }
}

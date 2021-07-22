using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float sensitivity = 1000.0f;

    //Y limit
    [SerializeField] private float yMaxLimit = 45.0f;
    [SerializeField] private float yMinLimit = -45.0f;
    float yRotCounter = 0.0f;

    //X limit
    [SerializeField] private float xMaxLimit = 45.0f;
    [SerializeField] private float xMinLimit = -45.0f;
    float xRotCounter = 0.0f;

    Transform player;

    private void Start()
    {
        player = Camera.main.transform;
    }

    void Update()
    {
        // Get X value and limit it
        xRotCounter += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        xRotCounter = Mathf.Clamp(xRotCounter, xMinLimit, xMaxLimit);

        // Get Y value and limit it
        yRotCounter += Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        yRotCounter = Mathf.Clamp(yRotCounter, yMinLimit, yMaxLimit);

        player.localEulerAngles = new Vector3(-yRotCounter, xRotCounter, 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFightingMove : MonoBehaviour
{
    public GameObject player;

    void Update()
    {
        Vector3 newPosition = player.transform.position;

        newPosition += Vector3.back * 3f + Vector3.up * 1.5f + Vector3.right * 1.2f;

        Camera.main.transform.position = newPosition;
    }
}

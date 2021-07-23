using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 destination;

    private void Start()
    {
        SetDestiny();
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, destination) < 1f)
        {
            SetDestiny();
        }

        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
    }

    private void SetDestiny()
    {
        Vector2 destX = GameObject.Find("Level").GetComponent<Targets>().bordersX;
        Vector2 destY = GameObject.Find("Level").GetComponent<Targets>().bordersY;
        Vector2 destZ = GameObject.Find("Level").GetComponent<Targets>().bordersZ;

        destination = new Vector3(
            Random.Range(destX.x, destX.y),
            Random.Range(destY.x, destY.y),
            Random.Range(destZ.x, destZ.y)
        );
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    [SerializeField] private Transform[] walls;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private float tossTime = 5f;
    [SerializeField] private float cTossTime = 0f;
    [SerializeField] private bool needToTossAgain;

    public string tossedValue = "";
    public bool NeedToTossAgain { get => needToTossAgain; set => needToTossAgain = value; }

    private void CollectPoint()
    {
        for (int i = 0; i < walls.Length; i++)
        {
            if (walls[i].TransformDirection(Vector3.forward).y >= 0.98 &&
                walls[i].TransformDirection(Vector3.forward).y <= 1.2f)
            {
                Debug.DrawRay(walls[i].position, walls[i].TransformDirection(Vector3.forward), Color.red);
                needToTossAgain = false;
                tossedValue = walls[i].name;
            }
        }

        if (needToTossAgain)
        {
            Toss();
        }
    }

    private void Toss()
    {
        needToTossAgain = true;

        float dirX = Random.Range(0, 500);
        float dirY = Random.Range(0, 500);
        float dirZ = Random.Range(0, 500);

        rigidbody.AddForce(transform.up * 500);
        rigidbody.AddTorque(dirX, dirY, dirZ);
    }

    private void Start()
    {
        needToTossAgain = true;

        rigidbody = GetComponent<Rigidbody>();

        Toss();
    }

    private void Update()
    {
        cTossTime += Time.deltaTime;

        if (cTossTime >= tossTime)
        {
            if (rigidbody.velocity == Vector3.zero && rigidbody.angularVelocity == Vector3.zero)
            {
                CollectPoint();
            }
        }

        if (Vector3.Distance(transform.parent.position, transform.position) > 5)
        {
            transform.position = transform.parent.position;
            transform.rotation = Quaternion.identity;
            Toss();
        }
    }
}

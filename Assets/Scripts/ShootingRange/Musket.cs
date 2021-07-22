using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musket : MonoBehaviour
{
    [SerializeField] private Transform endOfMusket;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float reloadTime;
    [SerializeField] private Camera camera;

    private void Update()
    {
        if (reloadTime <= 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
                reloadTime = 1;
            }
        }

        reloadTime -= Time.deltaTime;
    }

    private void Shoot()
    {
        RaycastHit hit;

        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit))
        {
            var target = hit.transform.GetComponent<Target>();

            if (target != null)
            {
                Debug.Log("target");
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musket : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float reloadTime;
    [SerializeField] private Camera camera;
    [SerializeField] private ParticleSystem[] particleSystem;

    private void Update()
    {
        if (reloadTime <= 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
                animator.SetTrigger("IsShooting 0");
                reloadTime = 2.5f + 0.375f;

                ParticleSystem.EmitParams emitOverride = new ParticleSystem.EmitParams();
                emitOverride.startLifetime = 10f;

                for (int i = 0; i < particleSystem.Length; i++)
                {
                    particleSystem[i].Emit(emitOverride, 7);
                    particleSystem[i].transform.position = GameObject.Find("muszkiet_z_rekoma").transform.position;
                }
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
                Destroy(hit.transform.gameObject);
                Targets.instance.TargetsCount--;
            }
        }
    }
}

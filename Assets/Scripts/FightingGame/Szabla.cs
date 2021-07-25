using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Szabla : MonoBehaviour
{
    public bool isHitting;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Enemy")
        {
            if (!other.gameObject.GetComponent<FigthingEntity>().isEnemyDefending)
            {
                isHitting = true;
            }
        }
        if (other.gameObject.name == "Player")
        {
            if (!other.gameObject.GetComponent<FigthingEntity>().isPlayerDefending)
            {
                isHitting = true;
            }
        }
    }
}

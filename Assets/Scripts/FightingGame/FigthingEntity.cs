using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigthingEntity : MonoBehaviour
{
    public Animator animator;
    public bool isThisPlayer;

    public float cTimeBetweenAttacks;
    public bool isAttacking;

    public int health;

    public GameObject hand;

    [SerializeField] private float enemyDefenceTime;
    public bool isEnemyDefending;
    public bool isPlayerDefending;

    public GamesEnds endScreen;

    // Update is called once per frame
    void Update()
    {
        if (cTimeBetweenAttacks > 0.8f)
        {
            isAttacking = false;
        }

        if (!isAttacking)
        {
            animator.SetBool("IsAttacking", false);
        }

        animator.SetBool("IsWalkingRight", false);
        animator.SetBool("IsWalkingLeft", false);

        animator.SetBool("IsDefending", false);

        if (isThisPlayer)
        {
            if (Input.GetKey(KeyCode.D))
            {
                animator.SetBool("IsWalkingRight", true);
                transform.position += new Vector3(1f, 0f, 0f) * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                animator.SetBool("IsWalkingLeft", true);
                transform.position -= new Vector3(1f, 0f, 0f) * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.Space))
            {
                animator.SetBool("IsAttacking", true);
                cTimeBetweenAttacks = 0f;
                isAttacking = true;
            }

            if (Input.GetKey(KeyCode.S))
            {
                animator.SetBool("IsDefending", true);
                isPlayerDefending = true;
            }
            else
            {
                isPlayerDefending = false;
            }
        }
        else
        {
            var player = GameObject.Find("Player");

            if (Vector3.Distance(player.transform.position, transform.position) > Random.Range(1f, 3f))
            {
                animator.SetBool("IsWalkingLeft", true);
                transform.position -= new Vector3(1f, 0f, 0f) * Time.deltaTime;
            }
            else if (Vector3.Distance(player.transform.position, transform.position) <= 0.6f)
            {
                animator.SetBool("IsWalkingRight", true);
                transform.position += new Vector3(1f, 0f, 0f) * Time.deltaTime;
            }
            
            if (Vector3.Distance(player.transform.position, transform.position) <= Random.Range(1f, 2f))
            {
                // Attack or defend
                if (!isEnemyDefending)
                {
                    if (Random.Range(0, 100) > 30)
                    {
                        animator.SetBool("IsAttacking", true);
                        cTimeBetweenAttacks = 0f;
                        isAttacking = true;
                        isEnemyDefending = false;
                    }
                }
            }
        }

        if (isAttacking)
        {
            cTimeBetweenAttacks += Time.deltaTime;
        }

        if (hand.GetComponent<Szabla>().isHitting)
        {
            hand.GetComponent<Szabla>().isHitting = false;
            Attack();
        }
    }

    private void Attack()
    {
        if (isThisPlayer)
        {
            GameObject.Find("Enemy").GetComponent<FigthingEntity>().health--;
            if (GameObject.Find("Enemy").GetComponent<FigthingEntity>().health == 0)
            {
                endScreen.gameObject.SetActive(true);
                endScreen.verdict = true;
            }
        }
        else
        {
            GameObject.Find("Player").GetComponent<FigthingEntity>().health--;
            if (GameObject.Find("Player").GetComponent<FigthingEntity>().health == 0)
            {
                endScreen.gameObject.SetActive(true);
                endScreen.verdict = false;
            }
        }
    }
}

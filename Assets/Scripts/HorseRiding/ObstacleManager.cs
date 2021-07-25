using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GameObject spaceImage;
    public bool isThisBush;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.name == "Player")
        {
            spaceImage.SetActive(true);

            if (Input.GetKey(KeyCode.Space) && isThisBush)
            {
                other.gameObject.GetComponent<EntityManager>().playerAnimator.SetTrigger("Attack");
                Destroy(this.gameObject);
            }
            else if(Input.GetKey(KeyCode.Space) && !isThisBush)
            {
                other.gameObject.GetComponent<EntityManager>().playerAnimator.SetTrigger("Jump");
                other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 6, 0), ForceMode.Impulse);
                gameObject.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "Enemy")
        {
            bool reaction = other.gameObject.GetComponent<EntityManager>().CheckForReaction();
    
            if (isThisBush && reaction)
            {
                other.gameObject.GetComponent<EntityManager>().playerAnimator.SetTrigger("Attack");
                Destroy(this.gameObject);
            }
            else if (!isThisBush && reaction)
            {
                other.gameObject.GetComponent<EntityManager>().playerAnimator.SetTrigger("Jump");
                other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(1, 6, 0), ForceMode.Impulse);
                gameObject.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name == "Player")
        {
            collision.gameObject.GetComponent<EntityManager>().velocity = 0.1f;
            Destroy(this.gameObject);
        }
        else if (collision.transform.name == "Enemy")
        {
            collision.gameObject.GetComponent<EntityManager>().velocity = 0.1f;
            Destroy(this.gameObject);
        }
    }
}

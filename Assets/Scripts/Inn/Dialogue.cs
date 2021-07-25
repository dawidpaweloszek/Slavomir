using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI dialogText;
    public TextMeshProUGUI nameText;
    public string[] lines;
    public string[] names;
    public float textSpeed;
    public int index;
    public Animator animator;

    public GameObject camera;


    public void Enable(Transform player, Transform second)
    {
        float x = player.position.x + second.position.x;
        float y = player.position.y + second.position.y;
        float z = player.position.z + second.position.z;

        Vector3 newPos = new Vector3(x / 2, y / 2, z / 2);

        Debug.Log(newPos);

        camera.SetActive(true);
        camera.transform.position = newPos + player.TransformDirection(Vector3.right) * 2.5f + player.TransformDirection(Vector3.up);
        camera.transform.eulerAngles = player.eulerAngles;
        camera.transform.eulerAngles += new Vector3(0f, -90f, 0f);

        dialogText.text = string.Empty;
        nameText.text = string.Empty;

        StartDialogue();
    }

    private void OnDisable()
    {
        camera.SetActive(false);
        GameObject.Find("Player").GetComponent<ThirdPersonMovement>().isPlayerInDialogue = false;

        dialogText.text = "";
        nameText.text = "";
        lines = new string[0];
        names = new string[0];
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dialogText.text == lines[index])
                NextLine();
            else
            {
                StopAllCoroutines();
                dialogText.text = lines[index];
            }
        }
    }

    private void StartDialogue()
    {
        index = 0;

        names = new string[lines.Length];

        // Split lines into name and text
        for (int i = 0; i < lines.Length; i++)
        {
            string[] tmp = lines[i].Split('>');

            names[i] = tmp[0];
            lines[i] = tmp[1];
        }

        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        nameText.text = names[index];

        // Type each character 1 by 1
        foreach (char c in lines[index].ToCharArray())
        {
            dialogText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
    
    private void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            dialogText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDialogue : MonoBehaviour
{
    public GameObject spaceImage;
    public float distance;
    public DialoguesLibrary library;
    public GameObject dialogueUI;
    public RaycastHit hit;

    // Update is called once per frame
    void Update()
    {
        spaceImage.gameObject.SetActive(false);

        if (!gameObject.GetComponent<ThirdPersonMovement>().isPlayerInDialogue)
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distance))
            {
                Debug.DrawLine(transform.position, hit.transform.position, Color.red);

                var dialogable = hit.transform.gameObject.GetComponent<Dialogable>().shaftOfLight;

                if (dialogable.activeInHierarchy)
                {
                    spaceImage.gameObject.SetActive(true);

                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        BeginDialogue();
                        spaceImage.gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    private void BeginDialogue()
    {
        var textFile = library.dialoguesList[library.dialogueIndex];

        if (textFile != null)
        {
            // Split text file into lines
            string[] dialogLines = (textFile.text.Split('\n'));

            dialogueUI.SetActive(true); 

            gameObject.GetComponent<ThirdPersonMovement>().isPlayerInDialogue = true;

            dialogueUI.GetComponent<Dialogue>().index = 0;
            dialogueUI.GetComponent<Dialogue>().lines = dialogLines;
            dialogueUI.GetComponent<Dialogue>().Enable(transform, hit.transform);
        }

        library.dialogueIndex++;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartDialogue : MonoBehaviour
{
    public GameObject spaceImage;
    public float distance;
    public DialoguesLibrary library;
    public GameObject dialogueUI;
    public RaycastHit hit;
    public Animator animatorFromHit;

    public int indexOfDialogueToNextScene;
    public string nameOfTheNextScene;

    // Update is called once per frame
    void Update()
    {
        if (!dialogueUI.activeInHierarchy && SaveAndLoadSystem.LoadData().dialogueIndex == indexOfDialogueToNextScene)
        {
            SceneManager.LoadScene(nameOfTheNextScene, LoadSceneMode.Single);
        }

        if (!gameObject.GetComponent<ThirdPersonMovement>().isPlayerInDialogue && animatorFromHit != null)
            animatorFromHit.SetBool("IsSpeaking", false);

        spaceImage.gameObject.SetActive(false);

        if (!gameObject.GetComponent<ThirdPersonMovement>().isPlayerInDialogue)
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distance))
            {
                Debug.DrawLine(transform.position, hit.transform.position, Color.red);

                var dialogable = hit.transform.gameObject.GetComponent<Dialogable>();

                if (dialogable.shaftOfLight.activeInHierarchy)
                {
                    spaceImage.gameObject.SetActive(true);

                    //hit.transform.gameObject.transform.LookAt(transform.position + Vector3.up * 1.5f);

                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        animatorFromHit = dialogable.animator;
                        BeginDialogue();

                        animatorFromHit.SetBool("IsSpeaking", true);

                        spaceImage.gameObject.SetActive(false);
                    }
                }
            }
        }


    }

    private void BeginDialogue()
    {
        var textFile = library.dialoguesList[SaveAndLoadSystem.LoadData().dialogueIndex];
        SaveAndLoadSystem.SaveData(SaveAndLoadSystem.LoadData().dialogueIndex + 1, -1);

        if (textFile != null)
        {
            // Split text file into lines
            string[] dialogLines = (textFile.text.Split('\n'));

            dialogueUI.SetActive(true);

            gameObject.GetComponent<ThirdPersonMovement>().isPlayerInDialogue = true;

            dialogueUI.GetComponent<Dialogue>().index = 0;
            dialogueUI.GetComponent<Dialogue>().lines = dialogLines;
            dialogueUI.GetComponent<Dialogue>().Enable(transform, hit.transform);
            dialogueUI.GetComponent<Dialogue>().animator = animatorFromHit;
        }
    }
}

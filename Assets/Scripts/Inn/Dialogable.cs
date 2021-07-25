using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogable : MonoBehaviour
{
    public GameObject shaftOfLight;
    public DialoguesLibrary dialogues;
    public int[] dialogIndices;
    public Animator animator;
    public int animationIndex;

    private void Update()
    {
        shaftOfLight.SetActive(false);

        for (int i = 0; i < dialogIndices.Length; i++)
        {
            if (dialogIndices[i] == SaveAndLoadSystem.LoadData().dialogueIndex)
                shaftOfLight.SetActive(true);
        }
    }
}

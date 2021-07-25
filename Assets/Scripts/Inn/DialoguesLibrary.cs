using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialoguesLibrary", menuName = "ScriptableObjects/DialoguesLibrary", order = 1)]
public class DialoguesLibrary : ScriptableObject
{
    public int dialogueIndex;
    public List<TextAsset> dialoguesList;
}
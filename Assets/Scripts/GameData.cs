using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int dialogueIndex;
    public int points;

    public GameData(int dialogueIndex, int points)
    {
        if (dialogueIndex != -1)
            this.dialogueIndex = dialogueIndex;
        if (points != -1)
            this.points = points;
    }
}

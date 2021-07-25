using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveAndLoadSystem
{
    public static void SaveData(int i, int p)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/szlachcic.bin";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData data = new GameData(i, p);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static GameData LoadData()
    {
        string path = Application.persistentDataPath + "/szlachcic.bin";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameData data = formatter.Deserialize(stream) as GameData;

            stream.Close();

            Debug.Log(data.dialogueIndex + "\t" + data.points);

            return data;
        }
        else
        {
            return null;
        }
    }
}

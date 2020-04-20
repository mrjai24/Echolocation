using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveManager
{
    public static string path = Application.persistentDataPath + "/save.sav";
    public static void SaveData (GameManager save)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fstream = new FileStream(path, FileMode.Create);
        SaveData data = new SaveData(save);
        formatter.Serialize(fstream, data);
        fstream.Close();
    }

    public static SaveData LoadData()
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream ofstream = new FileStream(path, FileMode.Open);
            SaveData data = formatter.Deserialize(ofstream) as SaveData;
            ofstream.Close();
            Debug.Log("Loaded " + path);
            return data;
        }
        else
        {
            Debug.LogWarning("Save file not found - " + path);
            return null;
        }
    }
}

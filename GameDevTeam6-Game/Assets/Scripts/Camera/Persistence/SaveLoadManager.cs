using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveLoadManager
{
    private static string filePath = Application.persistentDataPath + "/persistence.metag";
    public static void SaveGame() {

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(filePath, FileMode.Create);
        
        SaveData saveData = new SaveData();

        formatter.Serialize(stream, saveData);
        stream.Close();

        Debug.Log(filePath);
    }

    public static SaveData LoadGame() {
        if (File.Exists(filePath)) {

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(filePath, FileMode.Open);

            SaveData loadedData = formatter.Deserialize(stream) as SaveData;
            return loadedData as SaveData;

        } else {

            Debug.LogWarning("No Save File Found at File Path: " + filePath);
            return null;
        }
    }
}

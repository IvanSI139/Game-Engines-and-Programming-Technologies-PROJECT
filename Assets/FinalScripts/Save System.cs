using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    private static SaveData _saveData = new SaveData();
    
    [System.Serializable]

    public struct SaveData
    {
        public PlayerSaveData PlayerData;
    }

    public static string SaveFileName()
    {
        string saveFile = Application.persistentDataPath + "/quicksave " + ".save";
        return saveFile;
    }

    public static void Save()
    {
        Debug.Log("Save?");
        HandleSaveData();
        
        File.WriteAllText(SaveFileName(), JsonUtility.ToJson(_saveData, true));
    }

    private static void HandleSaveData()
    {
        Debug.Log("HandleSave?");
        GameManeger.Instance.PlayerController.Save(ref _saveData.PlayerData);
    }

    public static void Load()
    {
        string saveContent = File.ReadAllText(SaveFileName());

        _saveData = JsonUtility.FromJson<SaveData>(saveContent);
        HandleLoadData();
    }
    private static void HandleLoadData()
    {
        GameManeger.Instance.PlayerController.Load(_saveData.PlayerData);
    }
}

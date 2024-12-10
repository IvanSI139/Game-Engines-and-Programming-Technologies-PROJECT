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
        public PlayerHealtData HealthData;
        public List<MeleeEData> MeleeEDatas;
        public List<RangedEData> rangedEDatas;
        public List<EnemyrHealtData> EnemyrHealtDatas;
        public List<ProjectileData> ProjectileDatas;
        public List<EProjectileData> EProjectileDatas;

    }

    public static string SaveFileName()
    {
        string saveFile = Application.persistentDataPath + "/quicksave " + ".save";
        return saveFile;
    }

    public static void Save()
    {
        Debug.Log("Save?");
        Debug.Log(Application.persistentDataPath);
        HandleSaveData();
        
        File.WriteAllText(SaveFileName(), JsonUtility.ToJson(_saveData, true));
    }

    private static void HandleSaveData()
    {
        Debug.Log("HandleSave?");
        GameManeger.Instance.PlayerController.Save(ref _saveData.PlayerData);
        GameManeger.Instance.Health.Save(ref _saveData.HealthData);

        _saveData.MeleeEDatas = new List<MeleeEData>();
        foreach (var Menemy in FindObjectsOfType<enemy_AI>())
        {
            MeleeEData data = new MeleeEData();
            Menemy.Save(ref data);
            _saveData.MeleeEDatas.Add(data);
        }

        _saveData.rangedEDatas = new List<RangedEData>();
        foreach (var Renemy in FindObjectsOfType<rangedAI>())
        {
            RangedEData data = new RangedEData();
            Renemy.Save(ref data);
            _saveData.rangedEDatas.Add(data);
        }

        _saveData.EnemyrHealtDatas = new List<EnemyrHealtData>();
        foreach (var Henemy in FindObjectsOfType<EnemyHealth>())
        {
            EnemyrHealtData data = new EnemyrHealtData();
            Henemy.Save(ref data);
            _saveData.EnemyrHealtDatas.Add(data);
        }
        Debug.Log($"Number of enemies saved: {_saveData.EnemyrHealtDatas.Count}");

        _saveData.ProjectileDatas = new List<ProjectileData>();
        foreach (var Projectile in FindObjectsOfType<projectile>())
        {
            ProjectileData data = new ProjectileData();
            Projectile.Save(ref data);
            _saveData.ProjectileDatas.Add(data);
        }

        _saveData.EProjectileDatas = new List<EProjectileData>();
        foreach (var EProjectile in FindObjectsOfType<EnemyProjectile>())
        {
            EProjectileData data = new EProjectileData();
            EProjectile.Save(ref data);
            _saveData.EProjectileDatas.Add(data);
        }
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
        GameManeger.Instance.Health.Load(_saveData.HealthData);

        var Menemies = FindObjectsOfType<enemy_AI>();
        for (int i = 0; i < Menemies.Length; i++)
        {
            if (i < _saveData.MeleeEDatas.Count)
            {
                Menemies[i].Load(_saveData.MeleeEDatas[i]);
            }
        }

        var Renemies = FindObjectsOfType<rangedAI>();
        for (int i = 0; i < Renemies.Length; i++)
        {
            if (i < _saveData.rangedEDatas.Count)
            {
                Renemies[i].Load(_saveData.rangedEDatas[i]);
            }
        }



        var Henemies = FindObjectsOfType<EnemyHealth>();
        for (int i = 0; i < Henemies.Length; i++)
        {
            if (i < _saveData.EnemyrHealtDatas.Count)
            {
                Henemies[i].Load(_saveData.EnemyrHealtDatas[i]);
            }
        }

        var Projectile = FindObjectsOfType<projectile>();
        for (int i = 0; i < Projectile.Length; i++)
        {
            if (i < _saveData.ProjectileDatas.Count)
            {
                Projectile[i].Load(_saveData.ProjectileDatas[i]);
            }
        }

        var EProjectile = FindObjectsOfType<EnemyProjectile>();
        for (int i = 0; i < EProjectile.Length; i++)
        {
            if (i < _saveData.EProjectileDatas.Count)
            {
                EProjectile[i].Load(_saveData.EProjectileDatas[i]);
            }
        }
    }
}

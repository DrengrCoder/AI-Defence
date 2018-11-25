using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveAndLoad : MonoBehaviour {

    private SaveInfo _saveInfo = new SaveInfo();

    [SerializeField]
    private SaveData _saveData;
    [SerializeField]
    private Upgrades _upgrades;

    [SerializeField]
    private string dataFilePath = "/SaveData/data.json";

    private void Awake()
    {
        Load();
    }

    public void Load()
    {
        string filePath = Application.dataPath + dataFilePath;

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            _saveInfo = JsonUtility.FromJson<SaveInfo>(dataAsJson);
        }
        else
        {
            _saveInfo = new SaveInfo();
        }

        _saveData.PlayerScraps = _saveInfo.PlayerScraps;
        _saveData.LevelsUnlocked = _saveInfo.LevelsUnlocked;

        _upgrades.MaxHealthPointer = _saveInfo.MaxHealthPointer;
        _upgrades.SpeedPointer = _saveInfo.SpeedPointer;
    }

    public void Save()
    {
        _saveInfo.PlayerScraps = _saveData.PlayerScraps;
        _saveInfo.LevelsUnlocked = _saveData.LevelsUnlocked;

        _saveInfo.MaxHealthPointer = _upgrades.MaxHealthPointer;
        _saveInfo.SpeedPointer = _upgrades.SpeedPointer;

        string dataAsJson = JsonUtility.ToJson(_saveInfo);

        string filePath = Application.dataPath + dataFilePath;
        File.WriteAllText(filePath, dataAsJson);
    }
}

[System.Serializable]
public class SaveInfo
{
    public int PlayerScraps = 0;
    public int LevelsUnlocked = 1;
    public int MaxHealthPointer = 0;
    public int SpeedPointer = 0;
}
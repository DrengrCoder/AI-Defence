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
    private Settings _settings;

    [SerializeField]
    private string dataFilePath = "/data.json";

    private void Awake()
    {
        Load();
    }

    public void Load()
    {
        string filePath = Application.persistentDataPath + dataFilePath;

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
        _upgrades.Guns = _saveInfo.Guns;

        _settings.Sensitivity = _saveInfo.Sensitivity;
        _settings.MasterVol = _saveInfo.MasterVol;
        _settings.MusicVol = _saveInfo.MusicVol;
        _settings.SFXVol = _saveInfo.SFXVol;
    }

    public void Save()
    {
        _saveInfo.PlayerScraps = _saveData.PlayerScraps;
        _saveInfo.LevelsUnlocked = _saveData.LevelsUnlocked;

        _saveInfo.MaxHealthPointer = _upgrades.MaxHealthPointer;
        _saveInfo.SpeedPointer = _upgrades.SpeedPointer;
        _saveInfo.Guns = _upgrades.Guns;

        _saveInfo.Sensitivity = _settings.Sensitivity;
        _saveInfo.MasterVol = _settings.MasterVol;
        _saveInfo.MusicVol = _settings.MusicVol;
        _saveInfo.SFXVol = _settings.SFXVol;

        string dataAsJson = JsonUtility.ToJson(_saveInfo);

        string filePath = Application.persistentDataPath + dataFilePath;
        File.WriteAllText(filePath, dataAsJson);
    }

    public void SaveSettings()
    {
        _saveInfo.Sensitivity = _settings.Sensitivity;
        _saveInfo.MasterVol = _settings.MasterVol;
        _saveInfo.MusicVol = _settings.MusicVol;
        _saveInfo.SFXVol = _settings.SFXVol;

        string dataAsJson = JsonUtility.ToJson(_saveInfo);

        string filePath = Application.persistentDataPath + dataFilePath;
        File.WriteAllText(filePath, dataAsJson);
    }
}

[System.Serializable]
public class SaveInfo
{
    //Player Save Info
    public int PlayerScraps = 0;
    public int LevelsUnlocked = 1;
    public int MaxHealthPointer = 0;
    public int SpeedPointer = 0;

    //Gun Info
    public List<string> Guns = new List<string>();

    //Settings Info
    public float Sensitivity = 10.0f;
    public float MasterVol = 10.0f;
    public float MusicVol = 10.0f;
    public float SFXVol = 10.0f;
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour {

    [SerializeField]
    private Slider _masterVol;
    [SerializeField]
    private Slider _musicVol;
    [SerializeField]
    private Slider _SFXVol;
    [SerializeField]
    private Slider _sensitivity;

    [SerializeField]
    private Settings _settings;
    [SerializeField]
    private SaveAndLoad _Save;

    [SerializeField]
    private PlayerController _Player;
    //Volume Controller

    public void Confirm()
    {
        _settings.Sensitivity = _sensitivity.value;
        _settings.MasterVol = _masterVol.value;
        _settings.MusicVol = _musicVol.value;
        _settings.SFXVol = _SFXVol.value;

        _Save.SaveSettings();
        _Player.MouseSensitivity = _sensitivity.value;
    }

    public void UpdateSliders()
    {
        _sensitivity.value = _settings.Sensitivity;
        _masterVol.value = _settings.MasterVol;
        _musicVol.value = _settings.MusicVol;
        _SFXVol.value = _settings.SFXVol;
    }
}

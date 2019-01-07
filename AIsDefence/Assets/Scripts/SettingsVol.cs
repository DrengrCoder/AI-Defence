using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsVol : MonoBehaviour {

    [SerializeField]
    private Slider _Master;
    [SerializeField]
    private Slider _Music;
    [SerializeField]
    private Slider _SFX;
    [SerializeField]
    private Slider _Sensitivity;

    [SerializeField]
    private Settings _settings;

    private void OnEnable()
    {
        _Master.value = _settings.MasterVol;
        _Music.value = _settings.MusicVol;
        _SFX.value = _settings.SFXVol;
        _Sensitivity.value = _settings.Sensitivity;
    }

    public void Confirm()
    {
        _settings.MasterVol = _Master.value;
        _settings.MusicVol = _Music.value;
        _settings.SFXVol = _SFX.value;
        _settings.Sensitivity = _Sensitivity.value;
    }
}

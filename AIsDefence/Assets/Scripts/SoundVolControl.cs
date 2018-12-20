using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundVolControl : MonoBehaviour {

    [SerializeField]
    private AudioSource[] _SFXSounds;//Scaled to sfx * master
    [SerializeField]
    private AudioSource _MusicSound;//Scaled to music * master
    [SerializeField]
    private AudioSource[] _UISounds;//Scaled directly to Master

    [SerializeField]
    private Settings _settings;

    void Start () {
        VolumeChange();
    }

    public void VolumeChange()
    {
        //_MusicSound.volume = _settings.MusicVol * _settings.MasterVol;

        for (int i = 0; i < _SFXSounds.Length; i++)
        {
            _SFXSounds[i].volume = _settings.SFXVol * _settings.MasterVol;
        }

        for (int i = 0; i < _UISounds.Length; i++)
        {
            _UISounds[i].volume = _settings.MasterVol;
        }
    }
}

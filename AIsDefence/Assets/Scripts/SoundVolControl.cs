using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundVolControl : MonoBehaviour {

    public List<AudioSource> SFXSounds = new List<AudioSource>();//Scaled to sfx * master
    [SerializeField]
    private AudioSource _MusicSound;//Scaled to music * master
    [SerializeField]
    private AudioSource[] _UISounds;//Scaled directly to Master

    [SerializeField]
    private Settings _settings;

    void Start () {
        VolumeChange();
    }

    public float GetCorrectSFXVol(AudioSource source)
    {
        SFXSounds.Add(source);
        float val = _settings.SFXVol * _settings.MasterVol;
        return val;
    }

    public void VolumeChange()
    {
        //_MusicSound.volume = _settings.MusicVol * _settings.MasterVol;

        for (int i = 0; i < SFXSounds.Count; i++)
        {
            SFXSounds[i].volume = _settings.SFXVol * _settings.MasterVol;
        }

        for (int i = 0; i < _UISounds.Length; i++)
        {
            _UISounds[i].volume = _settings.MasterVol;
        }
    }
}

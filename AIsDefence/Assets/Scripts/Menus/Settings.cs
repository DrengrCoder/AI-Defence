using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings")]
public class Settings : ScriptableObject
{
    public float Sensitivity = 0.0f;
    public float MasterVol = 0.0f;
    public float MusicVol = 0.0f;
    public float SFXVol = 0.0f;
}

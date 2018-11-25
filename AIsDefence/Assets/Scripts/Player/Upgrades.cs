using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades")]
public class Upgrades : ScriptableObject{

    public List<string> Guns = new List<string>();

    public int[] MaxHealths;
    public int MaxHealthPointer = 0;

    public int[] Speeds;
    public int SpeedPointer = 0;
}

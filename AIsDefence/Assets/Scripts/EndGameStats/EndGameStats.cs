using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EndGameStats")]
public class EndGameStats : ScriptableObject //Every stat need to start at 0 for easy reset purposes
{
    public float TimeTaken = 0.0f; //Needs to regonise a completed level

    //PlayerStats
    public int DamageTaken = 0;
    public int DamageDealt = 0;
    public int Shots = 0;
    public int Hits = 0;
    public int Kills = 0;

    public WaveStats[] WaveStats;
    public TowerStats[] TowerStats;
}

[System.Serializable]
public class WaveStats
{
    public int NumEnemies = 0;
    public int Bosses = 0;
    public int EnemiesKilled = 0;
    public int BossesKilled = 0;
}

[System.Serializable]//To Do
public class TowerStats
{
    public int Kills = 0;
    public int Shots = 0;
    public int Hits = 0;
    public int Damage = 0;
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour {

    [SerializeField]
    private EndGameStats _stats;

    private float _timer = 0.0f;
    public bool Completed = false;

    private void Start()
    {
        int w = _stats.WaveStats.Length;
        int t = _stats.TowerStats.Length;

        _stats.DamageDealt = 0;
        _stats.DamageTaken = 0;
        _stats.Shots = 0;
        _stats.Hits = 0;
        _stats.Kills = 0;

        for (int i = 0; i < w; i++)
        {
            _stats.WaveStats[i].NumEnemies = 0;
            _stats.WaveStats[i].Bosses = 0;
            _stats.WaveStats[i].EnemiesKilled = 0;
            _stats.WaveStats[i].BossesKilled = 0;
        }

        for (int i = 0; i < t; i++)
        {
            _stats.TowerStats[i].Kills = 0;
            _stats.TowerStats[i].Shots = 0;
            _stats.TowerStats[i].Hits = 0;
            _stats.TowerStats[i].Damage = 0;
        }
    }

    private void Update()
    {
        if (Completed == false)
        {
            _timer = _timer + Time.deltaTime;
            _stats.TimeTaken = _timer;
        }
    }
}

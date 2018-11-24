using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateWaveInfo : MonoBehaviour {

    [SerializeField]
    private EndGameStats _stats;

    [SerializeField]
    private Text _non;
    [SerializeField]
    private Text _enemies;
    [SerializeField]
    private Text _enemiesKilled;
    [SerializeField]
    private Text _bosses;
    [SerializeField]
    private Text _bossesKilled;

    public void Populate(int non)
    {
        _non.text = non.ToString();
        _enemies.text = _stats.WaveStats[non].NumEnemies.ToString();
        _bosses.text = _stats.WaveStats[non].Bosses.ToString();
        _enemiesKilled.text = _stats.WaveStats[non].EnemiesKilled.ToString();
        _bossesKilled.text = _stats.WaveStats[non].BossesKilled.ToString();
    }
}

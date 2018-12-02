using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatManager : MonoBehaviour {

    [SerializeField]
    private EndGameStats _stats;
    [SerializeField]
    private PlayerController _controller;
    [SerializeField]
    private SaveAndLoad _save;

    [SerializeField]
    private GameObject _endgame;
    [SerializeField]
    private EndScreenWaves _endgameWaves;
    [SerializeField]
    private EndScreenTowers _endgameTowers;

    [SerializeField]
    private Text _damageDealtText;
    [SerializeField]
    private Text _damageTakenText;
    [SerializeField]
    private Text _hitRateText;
    [SerializeField]
    private Text _killsText;
    [SerializeField]
    private Text _timerText;

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

    public void CompletedLevel()
    {
        Completed = true;
        _save.Save();

        _timerText.text = _stats.TimeTaken.ToString();
        _killsText.text = _stats.Kills.ToString();
        _damageTakenText.text = _stats.DamageTaken.ToString();
        _damageDealtText.text = _stats.DamageDealt.ToString();

        float hitRate = 0.0f;
        if (_stats.Shots != 0)
        {
            float hits = _stats.Hits;
            float shots = _stats.Shots;
            hitRate = (hits / shots) * 100.0f;
        }
        _hitRateText.text = hitRate.ToString();

        _endgame.SetActive(true);
        _endgameWaves.Populate();
        _endgameTowers.Populate();

        Cursor.visible = true;
        Time.timeScale = 0;
        _controller.Pause = true;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateTowerInfo : MonoBehaviour {

    [SerializeField]
    private EndGameStats _stats;

    [SerializeField]
    private Text _non;
    [SerializeField]
    private Text _damage;
    [SerializeField]
    private Text _kills;
    [SerializeField]
    private Text _hitRate;

    public void Populate(int non)
    {
        _non.text = non.ToString();
        _damage.text = _stats.TowerStats[non].Damage.ToString();
        _kills.text = _stats.TowerStats[non].Kills.ToString();

        float hitRate = 0.0f;
        if (_stats.TowerStats[non].Shots != 0)
        {
            float hits = _stats.TowerStats[non].Hits;
            float shots = _stats.TowerStats[non].Shots;
            hitRate = (hits / shots) * 100.0f;
        }
        _hitRate.text = hitRate.ToString();
    }
}

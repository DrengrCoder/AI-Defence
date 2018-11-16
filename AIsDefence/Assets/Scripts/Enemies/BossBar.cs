using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBar : MonoBehaviour {

    [SerializeField]
    private Slider _healthBar;

    private int _maxHealth;
    private int _health;

    private void Start()
    {
        _maxHealth = GetComponent<Enemy>()._maxHealth;
        _healthBar.maxValue = _maxHealth;
        _healthBar.value = _maxHealth;
    }

    private void OnEnable()
    {
        _maxHealth = GetComponent<Enemy>()._maxHealth;
        _healthBar.maxValue = _maxHealth;
        _healthBar.value = _maxHealth;
    }

    private void FixedUpdate()
    {
        _health = GetComponent<Enemy>().Health;
        _healthBar.value = _health;
    }
}

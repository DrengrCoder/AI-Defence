using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    [SerializeField]
    private int _maxHealth = 100;
    [SerializeField]
    private Slider _healthBar;

    public int Health = 100;

    private void Start()
    {
        _healthBar.maxValue = _maxHealth;
        Respawn();
    }

    private void OnEnable()
    {
        Health = _maxHealth;
        _healthBar.value = Health;
    }

    public void Respawn()
    {
        Health = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        Health = Health - damage;
        _healthBar.value = Health;

        if (Health <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        gameObject.SetActive(false);
    }
}

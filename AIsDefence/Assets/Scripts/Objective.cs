using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour {

    [SerializeField]
    private int _maxHealth;

    public int CurrentHealth;

    private void Start()
    {
        CurrentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth = CurrentHealth - damage;

        if (CurrentHealth <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        Debug.Log("Objective Destroyed, Game Over");
    }

}

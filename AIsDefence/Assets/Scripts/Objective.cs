﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour {

    [SerializeField]
    private int _maxHealth;
    [SerializeField]
    private StatManager _statManager;

    [SerializeField]
    private Color _lowercolour;
    [SerializeField]
    private Color _highercolour;

    public int CurrentHealth;

    private Material _material;

    private void Start()
    {
        CurrentHealth = _maxHealth;
        _material = GetComponent<Renderer>().material;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth = CurrentHealth - damage;

        float hdrIntensity = 1.0f / _maxHealth;
        hdrIntensity = hdrIntensity * CurrentHealth;

        Color lerpedColor = Color.Lerp(_lowercolour, _highercolour, hdrIntensity);
        _material.SetColor("_EmissionColor", lerpedColor);

        if (CurrentHealth <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        _statManager.CompletedLevel();
    }

}

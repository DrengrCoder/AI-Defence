using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    [SerializeField]
    private int _maxHealth = 100;
    [SerializeField]
    private Slider _healthBar;

    [SerializeField]
    private Upgrades _upgrades;

    [SerializeField]
    private float _switchWeaponTime = 0.3f;
    private float _switch = 0.3f;

    public List<GameObject> Guns = new List<GameObject>();
    private int _currentGun = 0;

    public int Health = 100;

    [SerializeField]
    private EndGameStats _stats;

    private void Start()
    {
        int healthPointer = _upgrades.MaxHealthPointer;
        _maxHealth = _upgrades.MaxHealths[healthPointer];

        Health = _maxHealth;
        _healthBar.value = Health;
    }

    private void OnEnable()
    {
        int healthPointer = _upgrades.MaxHealthPointer;
        _maxHealth = _upgrades.MaxHealths[healthPointer];

        Health = _maxHealth;
        _healthBar.value = Health;
    }

    public void UpgradeHealth()//Player heals when they upgrade health
    {
        int healthPointer = _upgrades.MaxHealthPointer;
        healthPointer = healthPointer + 1;
        _upgrades.MaxHealthPointer = healthPointer;

        _maxHealth = _upgrades.MaxHealths[healthPointer];

        Health = _maxHealth;
        _healthBar.value = Health;
    }

    public void TakeDamage(int damage)
    {
        Health = Health - damage;
        _healthBar.value = Health;

        _stats.DamageTaken = _stats.DamageTaken + damage;

        if (Health <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        gameObject.SetActive(false);
    }

    private void ChangeGun()
    {
        for (int i = 0; i < Guns.Count; i++)
        {
            if (_currentGun != i)
            {
                Guns[i].SetActive(false);
            }
            else
            {
                Guns[i].SetActive(true);
            }
        }
    }

    private void Update()
    {
        float switchWeapon = Input.GetAxis("Mouse ScrollWheel");

        if (_switch >= _switchWeaponTime)
        {
            if (switchWeapon > 0f) //Next
            {
                _switch = 0.0f;
                _currentGun = _currentGun + 1;

                if (_currentGun == Guns.Count)
                {
                    _currentGun = 0;
                }
                ChangeGun();
            }
            else if (switchWeapon < 0f) //Previous
            {
                _switch = 0.0f;
                _currentGun = _currentGun - 1;

                if (_currentGun < 0)
                {
                    _currentGun = Guns.Count - 1;
                }
                ChangeGun();
            }
        }
        else
        {
            _switch = _switch + Time.deltaTime;
        }
    }

}

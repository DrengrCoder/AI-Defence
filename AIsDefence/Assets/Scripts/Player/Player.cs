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
    private float _switchWeaponTime = 0.3f;
    private float _switch = 0.3f;

    public List<GameObject> Guns = new List<GameObject>();
    private int _currentGun = 0;

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

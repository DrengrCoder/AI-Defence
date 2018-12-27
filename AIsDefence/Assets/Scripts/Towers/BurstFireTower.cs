using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstFireTower : Tower {

    [SerializeField]
    private GameObject _bullet;

    [SerializeField]
    private int _force = 5000;

    [SerializeField]
    private int _damage = 2;
    [SerializeField]
    private int _damageUpgrade = 1;

    private float _roundDelay = 0.1f;
    [SerializeField]
    private float _burstDelay = 0.7f;
    private float _fireRate = 0.1f;

    [SerializeField]
    private float _fireRateUpgrade = 0.05f;

    [SerializeField]
    private int _healthUpgrade = 25;
    [SerializeField]
    private int _rangeUpgrade = 10;

    private int _burstCount = 0;
    private const int _maxBurst = 3;

    private TowerType _type = TowerType.BurstFire;


    private void OnTriggerEnter(Collider obj)
    {
        if (!obj.isTrigger && obj.tag == "Enemy")
        {
            _inRangeEnemies.Add(obj.gameObject);
            AllocateNewTarget();
        }
    }

    private void OnTriggerStay(Collider obj)
    {
        if (!obj.isTrigger && obj.tag == "Enemy" && _canFire)
        {
            Attack();
            _canFire = false;

            _fireRate = _roundDelay;

            if (++_burstCount >= _maxBurst)
            {
                _burstCount = 0;
                _fireRate = _burstDelay;
            }

        }
    }

    private void OnTriggerExit(Collider obj)
    {
        if (!obj.isTrigger && obj.tag == "Enemy")
        {
            _inRangeEnemies.Remove(obj.gameObject);
            if (obj == _currentTarget)
            {
                AllocateNewTarget();
            }
        }
    }

    private void Attack()
    {
        _projectileManager.FireProjectile(this.gameObject, _currentTarget.GetComponent<CapsuleCollider>(), _bullet, _force, _damage, Num);
    }


    public override void SetTowerFireRate(float rate)
    {
        _burstDelay = rate;
    }

    public override void SetTowerDamage(int damage)
    {
        _damage = damage;
    }

    public override float GetTowerFireRate(bool burstDelay)
    {
        if (burstDelay == true)
            return _fireRate;
        else
            return _burstDelay;
    }

    public override int GetTowerDamage()
    {
        return _damage;
    }


    public override int GetDamageUpgrade()
    {
        return this._damageUpgrade;
    }

    public override int GetHealthUpgrade()
    {
        return this._healthUpgrade;
    }

    public override float GetFirerateUpgrade()
    {
        return this._fireRateUpgrade;
    }

    public override int GetRadiusUpgrade()
    {
        return this._rangeUpgrade;
    }


    public override TowerType GetTowerType()
    {
        return _type;
    }
}

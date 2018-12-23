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

    private float _originalDelay = 0.1f;
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
        if (++_burstCount >= _maxBurst)
        {
            _burstCount = 0;
            _fireRate = _burstDelay;
            ResettingDelay = true;
            ResetDelayTo = _originalDelay;
        }

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

    public override float GetTowerFireRate()
    {
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

    public override int BaseDamage()
    {
        return 5;
    }

    public override float BaseFireRate()
    {
        return 0.7f;
    }

    public override int BaseRange()
    {
        return 50;
    }
}

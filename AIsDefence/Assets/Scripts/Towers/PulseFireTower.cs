using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseFireTower : Tower {

    [SerializeField]
    private int _damage = 2;
    [SerializeField]
    private int _damageUpgrade = 1;

    [SerializeField]
    private float _fireRate = 1.0f;
    [SerializeField]
    private float _fireRateUpgrade = 0.1f;

    [SerializeField]
    private int _healthUpgrade = 25;
    [SerializeField]
    private int _rangeUpgrade = 15;

    private TowerType _type = TowerType.PulseFire;

    [SerializeField]
    private EndGameStats _stats;

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
        foreach (GameObject enemy in _inRangeEnemies)
        {
            bool killed = enemy.GetComponent<Enemy>().TakeDamage(_damage);

            _stats.TowerStats[Num - 1].Damage = _stats.TowerStats[Num - 1].Damage + _damage;
            if (killed == true)
            {
                _stats.TowerStats[Num - 1].Kills = _stats.TowerStats[Num - 1].Kills + 1;
            }
        }
    }


    public override void SetTowerFireRate(float rate)
    {
        _fireRate = rate;
    }

    public override void SetTowerDamage(int damage)
    {
        _damage = damage;
    }

    public override float GetTowerFireRate()
    {
        return _fireRate;
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
        return 1;
    }

    public override float BaseFireRate()
    {
        return 1.0f;
    }

    public override int BaseRange()
    {
        return 35;
    }
}

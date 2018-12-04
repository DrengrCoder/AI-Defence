using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadFireTower : Tower {

    [SerializeField]
    private GameObject _bullet;

    [SerializeField]
    private int _force = 3000;

    private int _damage = 4;

    private float _fireRate = 0.75f;

    private TowerType _type = TowerType.SpreadFire;


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
        _projectileManager.FireSpreadProjectile(this.gameObject, _currentTarget.GetComponent<CapsuleCollider>(), _bullet, _force, _damage, Num);
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

    public override TowerType GetTowerType()
    {
        return _type;
    }

    public override int BaseDamage()
    {
        return 3;
    }

    public override float BaseFireRate()
    {
        return 0.75f;
    }

    public override int BaseRange()
    {
        return 30;
    }
}

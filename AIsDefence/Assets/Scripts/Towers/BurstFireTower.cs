using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstFireTower : TowerController {

    [SerializeField]
    private GameObject _bullet;

    [SerializeField]
    private int _force = 5000;
    [SerializeField]
    private int _damage = 5;

    private const float _originalDelay = 0.1f;
    private const float _burstDelay = 0.7f;

    private int _burstCount = 0;
    private const int _maxBurst = 3;

    void Awake()
    {
        AttackCooldown = _originalDelay;
    }

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
        if (!obj.isTrigger && obj.tag == "Enemy" && _canAttack)
        {
            Attack();
            _canAttack = false;
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
            AttackCooldown = _burstDelay;
            ResettingDelay = true;
            DelayReset = _originalDelay;
        }

        _projectileManager.FireProjectile(this.gameObject, _currentTarget.GetComponent<CapsuleCollider>(), _bullet, _force, _damage);
    }
}

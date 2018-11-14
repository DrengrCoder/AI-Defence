using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleFireTower : TowerController {

    [SerializeField]
    private GameObject _bullet;

    [SerializeField]
    private int _force = 4000;
    [SerializeField]
    private int _damage = 10;

    void Awake()
    {
        AttackCooldown = 0.5f;
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
        _projectileManager.FireProjectile(this.gameObject, _currentTarget.GetComponent<CapsuleCollider>(), _bullet, _force, _damage);
    }
}

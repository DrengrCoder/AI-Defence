using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeTower : TowerController {

    [SerializeField]
    private GameObject _bullet;

    [SerializeField]
    private int _force = 1500;
    [SerializeField]
    private int _damage = 5;

    void Awake()
    {
        AttackCooldown = 1.0f;
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

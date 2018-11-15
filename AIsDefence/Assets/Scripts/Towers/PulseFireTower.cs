using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseFireTower : TowerController {
    
    [SerializeField]
    private int _damage = 1;

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
        foreach (GameObject enemy in _inRangeEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(_damage);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemy : Enemy {

    public EnemyProjectilePool BulletPool;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name);
        if (other.gameObject == EnemyTarget)//attacks player and tower
        {
            Attack(other.gameObject);
            gameObject.GetComponent<NavMeshAgent>().SetDestination(transform.position);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == EnemyTarget)//attacks player and tower
        {
            Attack(other.gameObject);
        }
    }

    private void Attack(GameObject target)
    {
        if (CanAttack == true) {
            if (target.GetComponent<Objective>())
            {
                GameObject bullet = BulletPool.GetBullet(Damage);
                bullet.transform.position = this.transform.position;
                bullet.SetActive(true);
                bullet.GetComponent<RangedEnemyProjectile>().Shoot(target.transform.position);
                CanAttack = false;
            }
        }
    }
}

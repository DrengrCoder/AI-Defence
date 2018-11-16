using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemy : Enemy {

    public EnemyProjectilePool BulletPool;
    [SerializeField]
    private GameObject _bulletSpawn;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name);
        if (other.gameObject == EnemyTarget)//attacks player and tower
        {
            Attack(other.gameObject);
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
                bullet.transform.position = _bulletSpawn.transform.position;
                bullet.SetActive(true);
                bullet.GetComponent<RangedEnemyProjectile>().Shoot(target.transform.position);
                CanAttack = false;
            }
        }
    }
}

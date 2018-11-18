using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemy : Enemy {

    public EnemyProjectilePool BulletPool;
    [SerializeField]
    private GameObject _bulletSpawn;

    [SerializeField]
    private string _playerTag;
    [SerializeField]
    private string _towerTag;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == EnemyTarget)
        {
            FaceObjective = EnemyTarget;
            Attack(other.gameObject);
        }
        else if ((other.gameObject.tag == _playerTag) || (other.gameObject.tag == _towerTag))
        {
            FaceObjective = other.gameObject;
            Attack(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == EnemyTarget)
        {
            FaceObjective = EnemyTarget;
            Attack(other.gameObject);
        }
        else if ((other.tag == _playerTag) || (other.tag == _towerTag))
        {
            FaceObjective = other.gameObject;
            Attack(other.gameObject);
        }
    }

    private void Attack(GameObject target)
    {
        if (CanAttack == true)
        {
            GameObject bullet = BulletPool.GetBullet(Damage);
            bullet.transform.position = _bulletSpawn.transform.position;
            bullet.SetActive(true);
            bullet.GetComponent<RangedEnemyProjectile>().Shoot(target.transform.position);
            CanAttack = false;
        }
    }
}

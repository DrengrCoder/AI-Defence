using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : Enemy {

    [SerializeField]
    private Melee _meleeWeapon;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == EnemyTarget)//attacks player
        {
            Attack(other.gameObject);
            GetComponent<NavMeshAgent>().isStopped = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == EnemyTarget)//attacks player
        {
            GetComponent<NavMeshAgent>().isStopped = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == EnemyTarget)//attacks player
        {
            Attack(other.gameObject);
        }
    }

    private void Attack(GameObject target)
    {
        if (CanAttack == true)
        {
            if (target.GetComponent<Objective>())
            {
                _meleeWeapon.MeleeAttack();
                CanAttack = false;
            }
        }
    }
}

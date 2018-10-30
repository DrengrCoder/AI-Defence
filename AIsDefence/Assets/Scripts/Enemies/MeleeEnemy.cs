using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : Enemy {

    [SerializeField]
    private StabbingMelee _meleeWeapon;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == EnemyTarget)//attacks player
        {
            Attack(other.gameObject);
            gameObject.GetComponent<NavMeshAgent>().updatePosition = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == EnemyTarget)//corrects movement
        {
            gameObject.GetComponent<NavMeshAgent>().updatePosition = true;
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

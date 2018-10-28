using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy {

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject == EnemyTarget)//attacks player
        {
            Attack(other.gameObject);
        }
    }

    private void Attack(GameObject target)
    {
        if (target.GetComponent<Objective>())
        {
            target.GetComponent<Objective>().TakeDamage(Damage);
        }

        Debug.Log("Melee Attack");
    }
}

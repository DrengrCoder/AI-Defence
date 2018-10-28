using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideEnemy : Enemy {

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject == EnemyTarget)
        {
            Attack();
        }
    }

    private void Attack()
    {
        if(EnemyTarget.GetComponent<Objective>())
        {
            EnemyTarget.GetComponent<Objective>().TakeDamage(Damage);
        }

        CreditsOnDeath = 0;
        Death();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideEnemy : Enemy {

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name);
        if (other.gameObject == EnemyTarget)//ignores players and towers
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

        CreditsOnDeath = 0;
        Death();
    }

}

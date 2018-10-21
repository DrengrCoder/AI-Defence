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
        Debug.Log("Attack");
        Death();
    }

}

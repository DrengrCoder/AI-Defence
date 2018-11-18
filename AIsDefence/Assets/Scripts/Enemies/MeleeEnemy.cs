using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : Enemy {

    [SerializeField]
    private Melee _meleeWeapon;

    [SerializeField]
    private string _playerTag;

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
            _meleeWeapon.MeleeAttack();
            CanAttack = false;
        }
    }

    public void Enrage()
    {
        GameObject newTarget = GameObject.FindGameObjectWithTag(_playerTag);

        EnemyTarget = newTarget;
        FaceObjective = newTarget;
        gameObject.GetComponent<NavMeshAgent>().SetDestination(EnemyTarget.transform.position);
    }

    private void FixedUpdate()
    {
        gameObject.GetComponent<NavMeshAgent>().SetDestination(EnemyTarget.transform.position);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour {

    public int Damage;
    public bool Attacking = false;
    public bool AttackMovement = false;


    public void MeleeAttack()
    {
        Attacking = true;
        AttackMovement = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if ((other.gameObject.GetComponent<Objective>()) && (Attacking == true))//attacks player
        {
            other.gameObject.GetComponent<Objective>().TakeDamage(Damage);

            Attacking = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.GetComponent<Objective>()) && (Attacking == true))//attacks player
        {
            other.gameObject.GetComponent<Objective>().TakeDamage(Damage);

            Attacking = false;
        }
    }
}

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
        if (Attacking == true)//attacks player
        {
            if (other.gameObject.GetComponent<Objective>())
            {
                other.gameObject.GetComponent<Objective>().TakeDamage(Damage);
            }
            else if (other.gameObject.GetComponent<Player>())
            {
                other.gameObject.GetComponent<Player>().TakeDamage(Damage);
            }

            Attacking = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Attacking == true)//attacks player
        {
            if (other.gameObject.GetComponent<Objective>())
            {
                other.gameObject.GetComponent<Objective>().TakeDamage(Damage);
            }
            else if (other.gameObject.GetComponent<Player>())
            {
                other.gameObject.GetComponent<Player>().TakeDamage(Damage);
            }

            Attacking = false;
        }
    }
}

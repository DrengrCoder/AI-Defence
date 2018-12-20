using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlicingMelee : Melee {

    [SerializeField]
    private GameObject _arm;

    [SerializeField]
    private float _upright = 90.0f;
    [SerializeField]
    private float _down = 1.0f;
    [SerializeField]
    private float _slashSpeed = 1.0f;

    private bool _stopArm = false;

    private void Update()
    {
        if (AttackMovement == true)
        {
            _arm.transform.Rotate(-_slashSpeed * Time.deltaTime, 0, 0);

            if (_arm.transform.localEulerAngles.x <= _down)
            {
                AttackMovement = false;
            }
        }
        else if (AttackMovement == false) 
        {
            if (_arm.transform.localEulerAngles.x < _upright){
                _arm.transform.Rotate(_slashSpeed * Time.deltaTime, 0, 0);
            }
        }
    }

    //I need both sets of istriggers for some reason as having only either set doesn't work
    //Don't ask me why I have fallen into depression from this fact

    private void OnTriggerStay(Collider other)
    {
        if (Attacking == true)//attacks player
        {
            if ((!other.gameObject.GetComponent<Enemy>()) && (!other.isTrigger))
            {
                if (other.gameObject.GetComponent<Objective>())
                {
                    other.gameObject.GetComponent<Objective>().TakeDamage(Damage);
                }

                if (other.gameObject.GetComponent<Player>())
                {
                    other.gameObject.GetComponent<Player>().TakeDamage(Damage);
                }

                Attacking = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Attacking == true)//attacks player
        {
            Debug.Log(other.gameObject.name);
            if ((!other.gameObject.GetComponent<Enemy>()) && (!other.isTrigger))
            {
                if (other.gameObject.GetComponent<Objective>())
                {
                    other.gameObject.GetComponent<Objective>().TakeDamage(Damage);
                }

                if (other.gameObject.GetComponent<Player>())
                {
                    other.gameObject.GetComponent<Player>().TakeDamage(Damage);
                }

                Attacking = false;
            }
        }
    }
}

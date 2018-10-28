using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StabbingMelee : MonoBehaviour{

    public int Damage;

    [SerializeField]
    private float _stabFowards = 0.5f;//on z

    private bool _attacking = false;
    private Vector3 _normalPos;
    private Vector3 _attackingPos;

    private void OnEnable()
    {
        _normalPos = transform.localPosition;

        Vector3 temp = transform.localPosition;
        temp.z = temp.z + _stabFowards;
        _attackingPos = temp;
    }

    public void MeleeAttack()
    {
        _attacking = true;
    }

    private void Update()
    {
        if (_attacking == true)
        {
            float step = _stabFowards * Time.deltaTime;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, _attackingPos, step);
        }
        else
        {
            float step = _stabFowards * Time.deltaTime;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, _normalPos, step);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Objective>())//attacks player
        {
            other.gameObject.GetComponent<Objective>().TakeDamage(Damage);

            _attacking = false;
        }
    }

}

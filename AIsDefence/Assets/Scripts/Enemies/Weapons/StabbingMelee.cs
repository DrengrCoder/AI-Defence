using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StabbingMelee : Melee{

    [SerializeField]
    private float _stabFowards = 0.5f;//on z

    private Vector3 _normalPos;
    private Vector3 _attackingPos;

    private void OnEnable()
    {
        _normalPos = transform.localPosition;

        Vector3 temp = transform.localPosition;
        temp.z = temp.z + _stabFowards;
        _attackingPos = temp;
    }

    private void Update()
    {
        if (AttackMovement == true)
        {
            float step = _stabFowards * Time.deltaTime;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, _attackingPos, step);

            if (transform.localPosition == _attackingPos)
            {
                AttackMovement = false;
            }

        }
        else
        {
            float step = _stabFowards * Time.deltaTime;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, _normalPos, step);
        }
    }
}

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
        Debug.Log(_arm.transform.localEulerAngles.x);

        if (AttackMovement == true)
        {
            _arm.transform.Rotate(-_slashSpeed * Time.deltaTime, 0, 0);

            if (_arm.transform.localEulerAngles.x <= _down)
            {
                AttackMovement = false;
            }
        }
        else if (!(_arm.transform.localEulerAngles.x >= _upright)) 
        {
            _arm.transform.Rotate(_slashSpeed * Time.deltaTime, 0, 0);
        }
    }

}

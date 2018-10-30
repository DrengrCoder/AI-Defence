using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float Speed = 10.0f;
    public float RotateSpeed = 150.0f;

    void Update()
    {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * RotateSpeed;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * Speed;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);
    }
}

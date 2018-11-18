using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField]
    private float _pxBounds = 0.0f;
    [SerializeField]
    private float _nxBounds = 0.0f;
    [SerializeField]
    private float _pzBounds = 0.0f;
    [SerializeField]
    private float _nzBounds = 0.0f;

    [SerializeField]
    private float _speed = 10.0f;

    public void Update () {

        float x = Input.GetAxis("Horizontal") * _speed * Time.deltaTime;
        float z = Input.GetAxis("Vertical") * _speed * Time.deltaTime;

        float futureX = transform.position.x + x;
        float futureZ = transform.position.z + z;

        if (futureX >= _pxBounds)
        {
            x = 0;
        }
        else if (futureX <= _nxBounds)
        {
            x = 0;
        }

        if (futureZ >= _pzBounds)
        {
            z = 0;
        }
        else if (futureZ <= _nzBounds)
        {
            z = 0;
        }

        transform.Translate(x, 0, z);
    }
}

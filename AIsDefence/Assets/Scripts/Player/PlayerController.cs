using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float Speed = 10.0f;
    public float MouseSensitivity = 150.0f;
    public bool Pause = false;

    [SerializeField]
    private Upgrades _upgrades;

    [SerializeField]
    private Camera _cam;

    private void OnEnable()
    {
        Cursor.visible = false;

        int speedPointer = _upgrades.SpeedPointer;
        Speed = _upgrades.Speeds[speedPointer];
    }

    private void OnDisable()
    {
        Cursor.visible = true;
    }

    public void UpgradeSpeed()
    {
        int speedPointer = _upgrades.SpeedPointer;
        speedPointer = speedPointer + 1;
        _upgrades.SpeedPointer = speedPointer;

        Speed = _upgrades.Speeds[speedPointer];
    }

    void Update()
    {
        if (Pause == false)
        {
            float x = Input.GetAxis("Horizontal") * Time.deltaTime * Speed;
            float z = Input.GetAxis("Vertical") * Time.deltaTime * Speed;

            float horizontal = Input.GetAxis("Mouse X") * MouseSensitivity;
            float vertical = -Input.GetAxis("Mouse Y") * MouseSensitivity;

            float nextRotation = _cam.gameObject.transform.eulerAngles.x + vertical;

            transform.Rotate(0, horizontal, 0);

            if (((nextRotation <= 80) && (nextRotation >= -80)) || ((nextRotation <= 440) && (nextRotation >= 280)))
            {
                _cam.gameObject.transform.Rotate(vertical, 0, 0);
            }

            transform.Translate(x, 0, z);

            if (Cursor.visible == true)
            {
                Cursor.visible = false;
            }
        }
    }
}

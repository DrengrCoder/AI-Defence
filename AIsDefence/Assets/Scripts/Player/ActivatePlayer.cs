using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePlayer : MonoBehaviour {

    [SerializeField]
    private GameObject _character;

    private string _button = "PlayCharacter";
    private bool _active = false;

    private void Update()
    {
        if (Input.GetButtonDown(_button))
        {
            _active = !_active;
            _character.SetActive(_active);
        }
    }
}

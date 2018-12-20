using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivatePlayer : MonoBehaviour {

    [SerializeField]
    private GameObject _character;
    [SerializeField]
    private TowerSelection _TowerSelect;
    [SerializeField]
    private Button _hideTowers;
    [SerializeField]
    private Button _ETP;
    [SerializeField]
    private Button _TUM;
    [SerializeField]
    private Button _JTA;
    [SerializeField]
    private Button _PS;

    private string _button = "PlayCharacter";
    private bool _active = false;

    private void Update()
    {
        if (Input.GetButtonDown(_button))
        {
            Character();
        }
    }

    public void Character()
    {
        _active = !_active;

        _character.SetActive(_active);

        _TowerSelect.DisableTowers(_active);

        _hideTowers.interactable = !(_active);
        _ETP.interactable = !(_active);
        _TUM.interactable = !(_active);
        _JTA.interactable = !(_active);
        _PS.interactable = !(_active);
    }
}

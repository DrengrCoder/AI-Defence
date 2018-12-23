using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivatePlayer : MonoBehaviour {

    [SerializeField]
    private GameObject _character;
    [SerializeField]
    private RadialMenuController _wheelMenuController;
    [SerializeField]
    private TowerSelection _TowerSelect;
    [SerializeField]
    private Button _hideTowers;
    [SerializeField]
    private Button _JTA;
    [SerializeField]
    private Button _PS;
    [SerializeField]
    private Button _SW;

    private string _button = "PlayCharacter";
    [HideInInspector]
    public bool _active = false;

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

        if (_active)
        {
            _wheelMenuController.DisableTowerWheel();
        }

        _TowerSelect.DisableTowers(_active);

        _hideTowers.interactable = !(_active);
        _JTA.interactable = !(_active);
        _PS.interactable = !(_active);
        _SW.interactable = !(_active);
    }
}

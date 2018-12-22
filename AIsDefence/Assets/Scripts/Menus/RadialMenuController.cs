using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialMenuController : MonoBehaviour {

    [SerializeField]
    private GameObject _baseRadialWheel;
    [SerializeField]
    private GameObject[] _extensionWheels;

    private Ray _ray;
    private RaycastHit _hit;
    
    void Update()
    {
        //ray cast
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        //check for left mouse down
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //ray cast and hit a target
            if (Physics.Raycast(_ray, out _hit))
            {
                //check if that target was a tower
                if (_hit.transform.tag == "Tower")
                {
                    Tower hitTower = _hit.transform.parent.GetComponent<Tower>();

                    //disable extension wheels to begin with
                    foreach (GameObject obj in _extensionWheels)
                    {
                        obj.SetActive(false);
                    }
                    
                    //check if the menu was already open over the hit tower...
                    if (hitTower.MenuActiveOverThis == true)
                    {
                        //deactivate the wheel if already active over hit tower
                        _baseRadialWheel.gameObject.SetActive(false);
                    }
                    else//if the menu is not over the hit tower...
                    {
                        //first deactivate the base wheel
                        _baseRadialWheel.gameObject.SetActive(false);
                        //move it to the mouse pointer (over the new hit tower)
                        _baseRadialWheel.transform.position = Input.mousePosition;
                        //reactivate the base wheel
                        _baseRadialWheel.gameObject.SetActive(true);
                    }

                    // === this will ensure the correct towers menu-status is toggled or reset ===
                    //find all towers currently spawned and iterate through
                    Tower[] towers = GameObject.FindObjectsOfType<Tower>();
                    foreach (Tower tower in towers)
                    {
                        //if this iteration is NOT the currently hit tower
                        if (tower != hitTower)
                        {
                            //reset its menu-active status
                            tower.MenuActiveOverThis = false;
                        }
                    }
                    //finally, toggle the hit towers menu-active status
                    hitTower.MenuActiveOverThis = !hitTower.MenuActiveOverThis;
                    // ===========================================================================
                }
            }
        }
    }
    
    public void ExtendUpgradeWheel(Button button)
    {
        _extensionWheels[1].SetActive(false);

        if (_extensionWheels[0].activeSelf == false)
        {
            _extensionWheels[0].GetComponent<RMF_RadialMenu>()._selectedTower = _hit.transform.parent.GetComponent<Tower>();
            _extensionWheels[0].transform.position = button.transform.position;
            _extensionWheels[0].SetActive(true);
        }
        else
        {
            _extensionWheels[0].SetActive(false);
        }
    }

    public void ExtendAttackWheel(Button button)
    {
        _extensionWheels[0].SetActive(false);

        if (_extensionWheels[1].activeSelf == false)
        {
            _extensionWheels[1].GetComponent<RMF_RadialMenu>()._selectedTower = _hit.transform.parent.GetComponent<Tower>();
            _extensionWheels[1].transform.position = button.transform.position;
            _extensionWheels[1].SetActive(true);
        }
        else
        {
            _extensionWheels[1].SetActive(false);
        }
    }
}

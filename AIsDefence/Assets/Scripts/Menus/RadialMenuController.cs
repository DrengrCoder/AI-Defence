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
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Physics.Raycast(_ray, out _hit))
            {
                if (_hit.transform.tag == "Tower")
                {
                    Tower hitTower = _hit.transform.parent.GetComponent<Tower>();

                    foreach (GameObject obj in _extensionWheels)
                    {
                        obj.SetActive(false);
                    }
                    
                    if (hitTower.MenuActiveOverThis == true)
                    {
                        _baseRadialWheel.gameObject.SetActive(false);
                    }
                    else
                    {
                        _baseRadialWheel.gameObject.SetActive(false);
                        _baseRadialWheel.transform.position = Input.mousePosition;
                        _baseRadialWheel.gameObject.SetActive(true);
                    }

                    Tower[] towers = GameObject.FindObjectsOfType<Tower>();
                    foreach (Tower tower in towers)
                    {
                        if (tower != hitTower)
                        {
                            tower.MenuActiveOverThis = false;
                        }
                    }
                    hitTower.MenuActiveOverThis = !hitTower.MenuActiveOverThis;
                }
            }
        }
    }
    
    public void ExtendUpgradeWheel(Button button)
    {
        _extensionWheels[1].SetActive(false);

        if (_extensionWheels[0].activeSelf == false)
        {
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
            _extensionWheels[1].transform.position = button.transform.position;
            _extensionWheels[1].SetActive(true);
        }
        else
        {
            _extensionWheels[1].SetActive(false);
        }
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

    public GameObject ShopMenu;

    [SerializeField]
    private PlayerController _controller;

    private bool _on = true;
    private bool _canShop = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            _canShop = true;
            Debug.Log("In");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            _canShop = false;
            Debug.Log("Out");
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Shop") && (_canShop == true))
        {
            SwitchState();
        }
    }

    public void SwitchState()
    {
        if (_on == true)
        {
            ShopMenu.SetActive(true);
            Time.timeScale = 0;
            _on = false;
            _controller.Pause = true;
            Cursor.visible = true;
        }
        else
        {
            ShopMenu.SetActive(false);
            Time.timeScale = 1;
            _on = true;
            _controller.Pause = false;
            Cursor.visible = false;
        }
    }
}

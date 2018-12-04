using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shop : MonoBehaviour {

    public GameObject ShopMenu;

    [SerializeField]
    private PlayerController _controller;
    [SerializeField]
    private GameObject _text;

    private bool _on = true;
    private bool _canShop = false;

    //ScrapShops
    [SerializeField]
    private WeaponShop _weaponShop;
    [SerializeField]
    private PlayerUpgradeShop _upgradeShop;

    public void UpdateShops()
    {
        _weaponShop.CheckPrices();
        _upgradeShop.CheckPrices();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            _canShop = true;
            _text.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            _canShop = false;
            _text.SetActive(false);
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
        if ((Time.timeScale == 0 && ShopMenu.activeSelf == true) || (Time.timeScale == 1))
        {
            if ((_on == true) && (Time.timeScale != 0))
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
}

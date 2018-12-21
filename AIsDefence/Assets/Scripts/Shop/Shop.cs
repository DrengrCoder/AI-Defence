using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Shop : MonoBehaviour {

    public GameObject ShopMenu;

    [SerializeField]
    private PlayerController _controller;

    [SerializeField]
    private TowerSelection _towerSelection;

    private bool _on = true;
    //ScrapShops
    [SerializeField]
    private WeaponShop _weaponShop;
    [SerializeField]
    private PlayerUpgradeShop _upgradeShop;

    private void Start()
    {
        UpdateShops();
    }

    public void UpdateShops()
    {
        _weaponShop.CheckPrices();
        _upgradeShop.CheckPrices();
    }

    void Update()
    {
        if (Input.GetButtonDown("Shop"))
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

                for (int i = 0; i < this._towerSelection._buttons.Length; i++)
                {
                    this._towerSelection._buttons[i].interactable = false;
                    this._towerSelection._shortCuts[i].GetComponent<Button>().interactable = false;
                }
            }
            else
            {
                ShopMenu.SetActive(false);
                Time.timeScale = 1;
                _on = true;
                _controller.Pause = false;
                Cursor.visible = false;
                this._towerSelection.CreditUpdated();
            }
        }
    }
}

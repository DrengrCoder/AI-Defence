using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponShop : MonoBehaviour {

    [SerializeField]
    private Player _player;

    [SerializeField]
    private Shop _shop;

    [SerializeField]
    private CreditBanks _bank;

    public GameObject[] Guns;
    public Button[] PurchaseButtons;

    [SerializeField]
    private int[] costs;

    private void OnEnable()
    {
        CheckPrices();
    }

    public void BuyWeapon(int gun)
    {
        _player.Guns.Add(Guns[gun]);
        _bank.MinusPlayerCredits(costs[gun]);

        costs[gun] = 0;
        _shop.UpdateShops();
    }

    public void CheckPrices()
    {
        int curr = _bank.PlayerCreditBank;

        for (int i = 0; i < costs.Length; i++)
        {
            if ((costs[i] <= curr) && (costs[i] > 0))
            {
                PurchaseButtons[i].interactable = true;
            }
            else
            {
                PurchaseButtons[i].interactable = false;
            }
        }
    }
}

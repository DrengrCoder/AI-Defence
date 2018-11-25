using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUpgradeShop : MonoBehaviour {

    [SerializeField]
    private CreditBanks _bank;

    [SerializeField]
    private Shop _shop;

    public Button[] PurchaseButtons;
    [SerializeField]
    private Text[] _costTexts;

    [SerializeField]
    private Costs[] _costs;

    [SerializeField]
    private Upgrades _upgrades;

    private void OnEnable()
    {
        for (int i = 0; i < _upgrades.MaxHealthPointer; i++)
        {
            _costs[0].NextCost();
        }
        _costTexts[0].text = _costs[0].CurrentCosts.ToString() + "QE";

        for (int i = 0; i < _upgrades.SpeedPointer; i++)
        {
            _costs[1].NextCost();
        }
        _costTexts[1].text = _costs[1].CurrentCosts.ToString() + "QE";

        CheckPrices();
    }

    public void BuyUpgrade(int upgrade)
    {
        _bank.MinusPlayerCredits(_costs[upgrade].CurrentCosts);

        _costs[upgrade].NextCost();

        _costTexts[upgrade].text = _costs[upgrade].CurrentCosts.ToString() + "QE";

        _shop.UpdateShops();
    }

    public void CheckPrices()
    {
        int curr = _bank.PlayerCreditBank;

        for (int i = 0; i < _costs.Length; i++)
        {
            if ((_costs[i].CurrentCosts <= curr) && (_costs[i].CurrentCosts > 0))
            {
                PurchaseButtons[i].interactable = true;
            }
            else
            {
                PurchaseButtons[i].interactable = false;
            }
        }
    }

    [System.Serializable]
    public class Costs
    {
        public int[] FutureCosts;//Controls num time it can be upgraded too
        public int Pointer = 0;

        public int CurrentCosts;

        public void NextCost()
        {
            Pointer = Pointer + 1;

            if (FutureCosts.Length > Pointer) {
                CurrentCosts = FutureCosts[Pointer];
            }
            else
            {
                CurrentCosts = 0;
            }
        }
    }

}

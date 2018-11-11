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

    private int[] _costPointer;

    private void OnEnable()
    {
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
        private int pointer = 0;

        public int CurrentCosts;

        public void NextCost()
        {
            pointer = pointer + 1;

            if (FutureCosts.Length > pointer) {
                CurrentCosts = FutureCosts[pointer];
            }
            else
            {
                CurrentCosts = 0;
            }
        }
    }

}

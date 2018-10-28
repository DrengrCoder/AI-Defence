using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditBanks : MonoBehaviour {

    [SerializeField]
    private int _creditBank = 0;
    [SerializeField]
    private Text _CreditDisplay;

    private void Start()
    {
        _CreditDisplay.text = _creditBank.ToString();
    }

    public void AddCredits(int credits)
    {
        _creditBank = _creditBank + credits;

        _CreditDisplay.text = _creditBank.ToString();
    }

    public void MinusCredits(int credits)
    {
        _creditBank = _creditBank - credits;

        _CreditDisplay.text = _creditBank.ToString();
    }

}

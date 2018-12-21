using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditBanks : MonoBehaviour {

    public int CreditBank = 0;
    public int PlayerCreditBank = 0;

    [SerializeField]
    private Text _CreditDisplay;
    [SerializeField]
    private TowerSelection _towerSelection;
    [SerializeField]
    private Text _PlayerCreditDisplay;
    [SerializeField]
    private SaveData _saveData;

    [SerializeField]
    private PlayerController _playerCharacter;

    private void Start()
    {
        PlayerCreditBank = _saveData.PlayerScraps;

        _CreditDisplay.text = CreditBank.ToString();
        _PlayerCreditDisplay.text = PlayerCreditBank.ToString();
    }

    public void AddCredits(int credits)
    {
        CreditBank = CreditBank + credits;

        _CreditDisplay.text = CreditBank.ToString();

        if (_playerCharacter.gameObject.activeSelf == false)
        {
            _towerSelection.CreditUpdated();
        }
    }

    public void MinusCredits(int credits)
    {
        CreditBank = CreditBank - credits;

        _CreditDisplay.text = CreditBank.ToString();

        _towerSelection.CreditUpdated();
    }

    public void AddPlayerCredits(int credits)
    {
        PlayerCreditBank = PlayerCreditBank + credits;
        _saveData.PlayerScraps = PlayerCreditBank;
        _PlayerCreditDisplay.text = PlayerCreditBank.ToString();
    }

    public void MinusPlayerCredits(int credits)
    {
        PlayerCreditBank = PlayerCreditBank - credits;
        _saveData.PlayerScraps = PlayerCreditBank;
        _PlayerCreditDisplay.text = PlayerCreditBank.ToString();
    }

}

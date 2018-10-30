using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerSelection : MonoBehaviour {
    
    public Button[] _buttons;

    [SerializeField]
    private int[] _costs;

    private bool _blackPressed = false;
    private bool _redPressed = false;
    private CreditBanks _bank;

	// Use this for initialization
	void Start ()
    {
        this._buttons[0].GetComponent<Button>().onClick.AddListener(delegate { ButtonClick(0); });
        this._buttons[1].GetComponent<Button>().onClick.AddListener(delegate { ButtonClick(1); });
        _bank = FindObjectOfType<CreditBanks>();
        CreditUpdated();
    }
	
	public void CreditUpdated() {
		for (int i = 0; i < _costs.Length; i++)
        {
            if (_costs[i] > _bank.CreditBank)
            {
                _buttons[i].interactable = false; 
            }
            else
            {
                _buttons[i].interactable = true;
            }
        }
	}

    void ButtonClick(int i)
    {
        this.ResetButtons();

        switch (i)
        {
            case 0:
                this._buttons[0].GetComponent<Button>().GetComponent<Image>().color = new Color(0, 255, 0);
                this._blackPressed = true;
                break;
            case 1:
                this._buttons[1].GetComponent<Button>().GetComponent<Image>().color = new Color(0, 255, 0);
                this._redPressed = true;
                break;
            default:
                break;
        }
    }

    public void ResetButtons()
    {
        this._buttons[0].GetComponent<Button>().GetComponent<Image>().color = new Color(255, 255, 255);
        this._buttons[1].GetComponent<Button>().GetComponent<Image>().color = new Color(255, 255, 255);
        this._blackPressed = false;
        this._redPressed = false;
    }

    public bool RedTowerSelected()
    {
        return this._redPressed;
    }

    public bool BlackTowerSelected()
    {
        return this._blackPressed;
    }
}

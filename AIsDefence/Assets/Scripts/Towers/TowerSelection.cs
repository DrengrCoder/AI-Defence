using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerSelection : MonoBehaviour {
    
    public Button[] _buttons;

    [SerializeField]
    private int[] _costs;

    private bool _singleFireTowerPressed = false;
    private bool _aoeTowerPressed = false;
    private bool _burstFireTowerPressed = false;
    private bool _pulseTowerPressed = false;
    private bool _spreadFireTowerPressed = false;

    private CreditBanks _bank;

	// Use this for initialization
	void Start ()
    {
        this._buttons[0].GetComponent<Button>().onClick.AddListener(delegate { ButtonClick(0); });//single fire
        this._buttons[1].GetComponent<Button>().onClick.AddListener(delegate { ButtonClick(1); });//AOE bomb
        this._buttons[2].GetComponent<Button>().onClick.AddListener(delegate { ButtonClick(2); });//burst fire
        this._buttons[3].GetComponent<Button>().onClick.AddListener(delegate { ButtonClick(3); });//pulse fire
        this._buttons[4].GetComponent<Button>().onClick.AddListener(delegate { ButtonClick(4); });//spread fire
        
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

        this._buttons[i].GetComponent<Button>().GetComponent<Image>().color = new Color(0, 255, 0);

        switch (i)
        {
            case 0://single fire
                this._singleFireTowerPressed = true;
                break;
            case 1://AOE bomb
                this._aoeTowerPressed = true;
                break;
            case 2://burst fire
                this._burstFireTowerPressed = true;
                break;
            case 3://pulse fire
                this._pulseTowerPressed = true;
                break;
            case 4://spread fire
                this._spreadFireTowerPressed = true;
                break;
            default:
                break;
        }
    }

    public void ResetButtons()
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            this._buttons[i].GetComponent<Button>().GetComponent<Image>().color = new Color(255, 255, 255);
        }

        this._singleFireTowerPressed = false;
        this._aoeTowerPressed = false;
        this._burstFireTowerPressed = false;
        this._pulseTowerPressed = false;
        this._spreadFireTowerPressed = false;
    }

    public bool AoeTowerSelected()
    {
        return this._aoeTowerPressed;
    }

    public bool SingleFireTowerSelected()
    {
        return this._singleFireTowerPressed;
    }

    public bool BurstFireTowerSelected()
    {
        return this._burstFireTowerPressed;
    }

    public bool PulseTowerSelected()
    {
        return this._pulseTowerPressed;
    }

    public bool SpreadTowerSelected()
    {
        return this._spreadFireTowerPressed;
    }
}

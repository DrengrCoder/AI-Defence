using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerSelection : MonoBehaviour {
    
    public Button[] _buttons;
    
    public Image[] _shortCuts;

    [SerializeField]
    private float _hiddenX = 0;
    [SerializeField]
    private float _visX = 0;
    [SerializeField]
    private Text _text;

    private bool _hidden = false;

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
        _bank = FindObjectOfType<CreditBanks>();
        CreditUpdated();
    }
	
	public void CreditUpdated() {
        if (Time.timeScale == 1)
        {
            for (int i = 0; i < _costs.Length; i++)
            {
                if (_costs[i] > _bank.CreditBank)
                {
                    _buttons[i].interactable = false;
                    _shortCuts[i].GetComponent<Button>().interactable = false;
                }
                else
                {
                    _buttons[i].interactable = true;
                    _shortCuts[i].GetComponent<Button>().interactable = true;
                }
            }
        }
	}

    public void ChangeTowerButtonStates()
    {
        float xVal = 0.0f;

        if (_hidden == true)
        {
            _hidden = false;
            _text.text = "Hide";

            xVal = _visX;
        }
        else if (_hidden == false)
        {
            _hidden = true;
            _text.text = "Show";

            xVal = _hiddenX;
        }

        for (int i = 0; i < _buttons.Length; i++)
        {
            float y = this._buttons[i].gameObject.GetComponent<RectTransform>().localPosition.y;
            Vector3 newPos = new Vector3(xVal, y, 0);
            this._buttons[i].gameObject.GetComponent<RectTransform>().localPosition = newPos;
        }
    }

    public void DisableTowers(bool disable)
    {
        if (disable == true)
        {
            _hidden = true;
            _text.text = "Show";

            float xVal = _hiddenX;

            for (int i = 0; i < _buttons.Length; i++)
            {
                float y = this._buttons[i].gameObject.GetComponent<RectTransform>().localPosition.y;
                Vector3 newPos = new Vector3(xVal, y, 0);
                this._buttons[i].gameObject.GetComponent<RectTransform>().localPosition = newPos;

                _shortCuts[i].GetComponent<Button>().interactable = false;
            }
        }
        else
        {
            CreditUpdated();
        }
    }

    public void ButtonClick(int i)
    {
        this.ResetButtons();

        this._buttons[i].GetComponent<Button>().GetComponent<Image>().color = new Color(0, 255, 0);
        _shortCuts[i].fillCenter = false;

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
            _shortCuts[i].fillCenter = true;
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

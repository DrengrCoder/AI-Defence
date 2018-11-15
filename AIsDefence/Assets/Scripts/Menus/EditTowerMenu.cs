using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditTowerMenu : MonoBehaviour {

    public enum AttackChoice { First = 1, Last = 2, HighHealth = 3, LowHealth = 4, HighDamage = 5, LowDamage = 6 };
    enum EditingTower { None = 1, SingleFire = 2, BurstFire = 3, SpreadFire = 4, AoeFire = 5, PulseFire = 6 };

    [SerializeField]
    private GameObject _editMenu;

    [SerializeField]
    private PlayerController _controller;

    private TowerSelection _towerSelection;

    private List<GameObject> _towers = new List<GameObject>();

    private bool _menuOn = false;
    private EditingTower _editingTower = EditingTower.None;

    private AttackChoice _singleFireTarget = AttackChoice.First;
    private AttackChoice _burstFireTarget = AttackChoice.First;
    private AttackChoice _spreadFireTarget = AttackChoice.First;
    private AttackChoice _aoeTarget = AttackChoice.First;

    private string TargetParameterText(AttackChoice option)
    {
        switch (option)
        {
            case AttackChoice.First:
                return "First";
            case AttackChoice.Last:
                return "Last";
            case AttackChoice.HighHealth:
                return "Most Health";
            case AttackChoice.LowHealth:
                return "Least Health";
            case AttackChoice.HighDamage:
                return "Most Damage";
            case AttackChoice.LowDamage:
                return "Least Damage";
            default:
                return "";
        }
    }

    void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey && e.type == EventType.KeyDown)
        {
            if (e.keyCode == KeyCode.Tab)//tab button is hotkey to edit menu
            {
                ToggleMenu();
            }
            else if (_menuOn)//any other keys whilst menu is on
            {
                CheckInputSelection(e);
                UpdateTree();
            }
        }
    }

    void Start()
    {
        _towerSelection = GameObject.Find("TowerSelectUI").GetComponent<TowerSelection>();
    }

    private void ToggleMenu()
    {
        if (((Time.timeScale == 0 && _editMenu.activeSelf == true) || (Time.timeScale == 1)) && (_controller.gameObject.activeSelf == false))
        {
            _editMenu.transform.GetChild(3).GetComponent<Text>().text = "Selected Tower";
            _editMenu.transform.GetChild(4).GetComponent<Text>().text = "";

            if (!_menuOn)
            {
                this._editMenu.SetActive(true);
                Time.timeScale = 0;
                _menuOn = !_menuOn;
            }
            else
            {
                this._editMenu.SetActive(false);
                Time.timeScale = 1;
                _menuOn = !_menuOn;
            }

            foreach (Button btn in this._towerSelection._buttons)
            {
                btn.interactable = !_menuOn;
            }
        }
    }
    
    private void CheckInputSelection(Event e)
    {
        switch (e.keyCode)//selecting tower
        {
            case KeyCode.Q:
                if (_editingTower != EditingTower.SingleFire)
                {
                    _editingTower = EditingTower.SingleFire;
                }
                else
                {
                    _editingTower = EditingTower.None;
                }
                break;
            case KeyCode.W:
                if (_editingTower != EditingTower.BurstFire)
                {
                    _editingTower = EditingTower.BurstFire;
                }
                else
                {
                    _editingTower = EditingTower.None;
                }
                break;
            case KeyCode.E:
                if (_editingTower != EditingTower.SpreadFire)
                {
                    _editingTower = EditingTower.SpreadFire;
                }
                else
                {
                    _editingTower = EditingTower.None;
                }
                break;
            case KeyCode.R:
                if (_editingTower != EditingTower.AoeFire)
                {
                    _editingTower = EditingTower.AoeFire;
                }
                else
                {
                    _editingTower = EditingTower.None;
                }
                break;
            default:
                break;
        }

        if (_editingTower != EditingTower.None)//if a tower is already selected for editing
        {
            switch (e.keyCode)//selecting attack parameters
            {
                case KeyCode.Alpha1:
                    UpdateAttackParameters(AttackChoice.First);
                    break;
                case KeyCode.Alpha2:
                    UpdateAttackParameters(AttackChoice.Last);
                    break;
                case KeyCode.Alpha3:
                    UpdateAttackParameters(AttackChoice.HighHealth);
                    break;
                case KeyCode.Alpha4:
                    UpdateAttackParameters(AttackChoice.LowHealth);
                    break;
                case KeyCode.Alpha5:
                    UpdateAttackParameters(AttackChoice.HighDamage);
                    break;
                case KeyCode.Alpha6:
                    UpdateAttackParameters(AttackChoice.LowDamage);
                    break;
                default:
                    break;
            }
        }
    }

    private void UpdateTree()
    {
        _editMenu.transform.GetChild(4).GetComponent<Text>().text = "Attack First Enemy (1)\nAttack Last Enemy (2)\nAttack Most Health (3)\nAttack Least Health (4)\nAttack Most Damage (5)\nAttack Least Damage (6)";

        switch (this._editingTower)
        {
            case EditingTower.SingleFire:
                _editMenu.transform.GetChild(3).GetComponent<Text>().text = "Single Fire Tower Selected - Targeting " + TargetParameterText(_singleFireTarget) + " Enemy";
                break;
            case EditingTower.BurstFire:
                _editMenu.transform.GetChild(3).GetComponent<Text>().text = "Burst Fire Tower Selected - Targeting " + TargetParameterText(_burstFireTarget) + " Enemy";
                break;
            case EditingTower.SpreadFire:
                _editMenu.transform.GetChild(3).GetComponent<Text>().text = "Spread Fire Tower Selected - Targeting " + TargetParameterText(_spreadFireTarget) + " Enemy";
                break;
            case EditingTower.AoeFire:
                _editMenu.transform.GetChild(3).GetComponent<Text>().text = "AOE Tower Selected - Targeting " + TargetParameterText(_aoeTarget) + " Enemy";
                break;
            default://assumed EditingTower.None;
                _editMenu.transform.GetChild(3).GetComponent<Text>().text = "No Selected Tower";
                _editMenu.transform.GetChild(4).GetComponent<Text>().text = "";
                break;
        }

    }

    private void UpdateAttackParameters(AttackChoice option)
    {
        switch (_editingTower)
        {
            case EditingTower.SingleFire:
                _singleFireTarget = option;
                break;
            case EditingTower.BurstFire:
                _burstFireTarget = option;
                break;
            case EditingTower.SpreadFire:
                _spreadFireTarget = option;
                break;
            case EditingTower.AoeFire:
                _aoeTarget = option;
                break;
            default:
                break;
        }

        foreach (GameObject tower in _towers)
        {
            tower.GetComponent<TowerController>().SetAttackOption((int)option);
        }

        _editingTower = EditingTower.None;
    }

    public void AddTower(GameObject tower)
    {
        this._towers.Add(tower);
    }

    public void RemoveTower(GameObject tower)
    {
        this._towers.Remove(tower);
    }
}

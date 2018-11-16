using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class EditTowerMenu : MonoBehaviour {

    public enum AttackChoice { First = 1, Last = 2, HighHealth = 3, LowHealth = 4, HighDamage = 5, LowDamage = 6 };
    enum TowerType { None = 1, SingleFire = 2, BurstFire = 3, SpreadFire = 4, AoeFire = 5, PulseFire = 6 };

    [SerializeField]
    private GameObject _editMenu;

    [SerializeField]
    private PlayerController _controller;

    private TowerSelection _towerSelection;

    private List<GameObject> _towers = new List<GameObject>();

    private bool _menuOn = false;
    private TowerType _editingTower = TowerType.None;

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
                return "Most-Health";
            case AttackChoice.LowHealth:
                return "Least-Health";
            case AttackChoice.HighDamage:
                return "Most-Damage";
            case AttackChoice.LowDamage:
                return "Least-Damage";
            default:
                return "";
        }
    }
    private AttackChoice TargetParameterEnum(string option)
    {
        switch (option)
        {
            case "First":
                return AttackChoice.First;
            case "Last":
                return AttackChoice.Last;
            case "Most-Health":
                return AttackChoice.HighHealth;
            case "Least-Health":
                return AttackChoice.LowHealth;
            case "Most-Damage":
                return AttackChoice.HighDamage;
            case "Least-Damage":
                return AttackChoice.LowDamage;
            default:
                return AttackChoice.First;
        }
    }

    public int CheckTargetParameters(int towertype)
    {
        switch ((TowerType)towertype)
        {
            case TowerType.SingleFire:
                return (int)_singleFireTarget;
            case TowerType.BurstFire:
                return (int)_burstFireTarget;
            case TowerType.SpreadFire:
                return (int)_spreadFireTarget;
            case TowerType.AoeFire:
                return (int)_aoeTarget;
            default:
                return 0;
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
            else if (e.keyCode == KeyCode.S && e.modifiers == EventModifiers.Shift)
            {
                SavePreferences();
            }
            else if (e.keyCode == KeyCode.L && e.modifiers == EventModifiers.Shift)
            {
                LoadPreferences();
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
                ResetText();
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
                if (_editingTower != TowerType.SingleFire)
                {
                    _editingTower = TowerType.SingleFire;
                }
                else
                {
                    _editingTower = TowerType.None;
                }
                break;
            case KeyCode.W:
                if (_editingTower != TowerType.BurstFire)
                {
                    _editingTower = TowerType.BurstFire;
                }
                else
                {
                    _editingTower = TowerType.None;
                }
                break;
            case KeyCode.E:
                if (_editingTower != TowerType.SpreadFire)
                {
                    _editingTower = TowerType.SpreadFire;
                }
                else
                {
                    _editingTower = TowerType.None;
                }
                break;
            case KeyCode.R:
                if (_editingTower != TowerType.AoeFire)
                {
                    _editingTower = TowerType.AoeFire;
                }
                else
                {
                    _editingTower = TowerType.None;
                }
                break;
            default:
                break;
        }

        if (_editingTower != TowerType.None)//if a tower is already selected for editing
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
            case TowerType.SingleFire:
                _editMenu.transform.GetChild(3).GetComponent<Text>().text = "Single Fire Tower Selected - Targeting " + TargetParameterText(_singleFireTarget) + " Enemy";
                break;
            case TowerType.BurstFire:
                _editMenu.transform.GetChild(3).GetComponent<Text>().text = "Burst Fire Tower Selected - Targeting " + TargetParameterText(_burstFireTarget) + " Enemy";
                break;
            case TowerType.SpreadFire:
                _editMenu.transform.GetChild(3).GetComponent<Text>().text = "Spread Fire Tower Selected - Targeting " + TargetParameterText(_spreadFireTarget) + " Enemy";
                break;
            case TowerType.AoeFire:
                _editMenu.transform.GetChild(3).GetComponent<Text>().text = "AOE Tower Selected - Targeting " + TargetParameterText(_aoeTarget) + " Enemy";
                break;
            default://assumed EditingTower.None;
                ResetText();
                break;
        }

    }
    private void ResetText()
    {
        _editingTower = TowerType.None;
        _editMenu.transform.GetChild(3).GetComponent<Text>().text = "No Selected Tower";
        _editMenu.transform.GetChild(4).GetComponent<Text>().text = "";
    }

    private void UpdateAttackParameters(AttackChoice option)
    {
        switch (_editingTower)
        {
            case TowerType.SingleFire:
                _singleFireTarget = option;
                break;
            case TowerType.BurstFire:
                _burstFireTarget = option;
                break;
            case TowerType.SpreadFire:
                _spreadFireTarget = option;
                break;
            case TowerType.AoeFire:
                _aoeTarget = option;
                break;
            default:
                break;
        }

        _editingTower = TowerType.None;
        UpdateTowers();
    }
    private void UpdateTowers()
    {

        foreach (GameObject tower in _towers)
        {
            switch (tower.GetComponent<TowerController>().ReturnTowerType())
            {
                case (int)TowerType.SingleFire:
                    tower.GetComponent<TowerController>().SetAttackOption((int)_singleFireTarget);
                    break;
                case (int)TowerType.BurstFire:
                    tower.GetComponent<TowerController>().SetAttackOption((int)_burstFireTarget);
                    break;
                case (int)TowerType.SpreadFire:
                    tower.GetComponent<TowerController>().SetAttackOption((int)_spreadFireTarget);
                    break;
                case (int)TowerType.AoeFire:
                    tower.GetComponent<TowerController>().SetAttackOption((int)_aoeTarget);
                    break;
                default:
                    break;
            }
        }

    }

    private void SavePreferences()
    {
        using (StreamWriter writer = new StreamWriter("Assets/Assets/SaveData/test.txt", false))
        {
            writer.WriteLine("Single - " + TargetParameterText(_singleFireTarget));
            writer.WriteLine("Burst - " + TargetParameterText(_burstFireTarget));
            writer.WriteLine("Spread - " + TargetParameterText(_spreadFireTarget));
            writer.WriteLine("AOE - " + TargetParameterText(_aoeTarget));

            writer.Close();
        }
    }
    private void LoadPreferences()
    {
        string line = "";

        using (StreamReader reader = new StreamReader("Assets/Assets/SaveData/test.txt", true))
        {
            while ((line = reader.ReadLine()) != null)
            {
                string attackString = line.Substring(line.LastIndexOf(" ") + 1);
                AttackChoice attackChoice = TargetParameterEnum(attackString);//the last word, being the attack choice as a string
                switch (line.Substring(0, line.IndexOf(" ")))//the first word, being the tower name
                {
                    case "Single":
                        _singleFireTarget = attackChoice;
                        break;
                    case "Burst":
                        _burstFireTarget = attackChoice;
                        break;
                    case "Spread":
                        _spreadFireTarget = attackChoice;
                        break;
                    case "AOE":
                        _aoeTarget = attackChoice;
                        break;
                    default:
                        break;
                }
            }

            UpdateTowers();

            reader.Close();
        }
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class EditTowerMenu : MonoBehaviour {

    [SerializeField]
    private GameObject _editMenu;

    [SerializeField]
    private PlayerController _playerCharacter;

    [SerializeField]
    private TowerSelection _towerSelection;

    [SerializeField]
    private GameObject _priorityButtonGroup;
    
    [SerializeField]
    private Button[] _editButtons;

    [SerializeField]
    private GameObject[] _priorityTexts;

    private List<GameObject> _towers = new List<GameObject>();

    private TowerType _selectedTower = TowerType.None;

    private bool _menuOn = false;

    private AttackChoice _burstFireTarget = AttackChoice.First;
    private AttackChoice _singleFireTarget = AttackChoice.First;
    private AttackChoice _spreadFireTarget = AttackChoice.First;
    private AttackChoice _aoeTarget = AttackChoice.First;

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
        }
    }

    private void Start()
    {
        SetTowerPriorityText();
        
        _priorityButtonGroup.transform.GetChild(0).GetComponent<Button>().GetComponentInChildren<Text>().text 
            = AttackChoiceUtils.GetDescription(AttackChoice.First);

        _priorityButtonGroup.transform.GetChild(1).GetComponent<Button>().GetComponentInChildren<Text>().text 
            = AttackChoiceUtils.GetDescription(AttackChoice.MostHealth);

        _priorityButtonGroup.transform.GetChild(2).GetComponent<Button>().GetComponentInChildren<Text>().text 
            = AttackChoiceUtils.GetDescription(AttackChoice.MostDamage);

        _priorityButtonGroup.transform.GetChild(3).GetComponent<Button>().GetComponentInChildren<Text>().text 
            = AttackChoiceUtils.GetDescription(AttackChoice.Last);

        _priorityButtonGroup.transform.GetChild(4).GetComponent<Button>().GetComponentInChildren<Text>().text 
            = AttackChoiceUtils.GetDescription(AttackChoice.LeastHealth);

        _priorityButtonGroup.transform.GetChild(5).GetComponent<Button>().GetComponentInChildren<Text>().text 
            = AttackChoiceUtils.GetDescription(AttackChoice.LeastDamage);
    }

    private void SetTowerPriorityText()
    {
        _priorityTexts[0].GetComponent<Text>().text = "Burst Fire Attacking:\n\n" + AttackChoiceUtils.GetDescription(_burstFireTarget);
        _priorityTexts[1].GetComponent<Text>().text = "Single Fire Attacking:\n\n" + AttackChoiceUtils.GetDescription(_singleFireTarget);
        _priorityTexts[2].GetComponent<Text>().text = "Spread Fire Attacking:\n\n" + AttackChoiceUtils.GetDescription(_spreadFireTarget);
        _priorityTexts[3].GetComponent<Text>().text = "AOE Fire Attacking:\n\n" + AttackChoiceUtils.GetDescription(_aoeTarget);
    }

    public void ToggleMenu()
    {
        if (((Time.timeScale == 0 && _editMenu.activeSelf == true) || (Time.timeScale == 1)) && (_playerCharacter.gameObject.activeSelf == false))
        {
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
                _selectedTower = TowerType.None;
                SetSelectedTower();
            }

            foreach (Button btn in this._towerSelection._buttons)
            {
                btn.interactable = !_menuOn;
            }
        }
    }

    public void ShowEditOptions(int i)
    {
        switch (i)
        {
            case 1:
                if (_selectedTower == TowerType.BurstFire)
                {
                    _selectedTower = TowerType.None;
                }
                else
                {
                    _selectedTower = TowerType.BurstFire;
                }
                break;
            case 2:
                if (_selectedTower == TowerType.SingleFire)
                {
                    _selectedTower = TowerType.None;
                }
                else
                {
                    _selectedTower = TowerType.SingleFire;
                }
                break;
            case 3:
                if (_selectedTower == TowerType.SpreadFire)
                {
                    _selectedTower = TowerType.None;
                }
                else
                {
                    _selectedTower = TowerType.SpreadFire;
                }
                break;
            case 4:
                if (_selectedTower == TowerType.AoeFire)
                {
                    _selectedTower = TowerType.None;
                }
                else
                {
                    _selectedTower = TowerType.AoeFire;
                }
                break;
            default:
                break;
        }

        SetSelectedTower();
    }
    private void SetSelectedTower()
    {
        //by default, reset all colours
        foreach (Button btn in _editButtons)
        {
            btn.GetComponent<Image>().color = new Color(255, 255, 255);
        }

        string selectedText = "No Tower Selected";

        _priorityButtonGroup.SetActive(true);

        switch (_selectedTower)
        {
            case TowerType.BurstFire:
                selectedText = "Burst Fire Tower Selected";
                _editButtons[0].GetComponent<Image>().color = new Color(0, 255, 0);
                break;
            case TowerType.SingleFire:
                selectedText = "Single Fire Tower Selected";
                _editButtons[1].GetComponent<Image>().color = new Color(0, 255, 0);
                break;
            case TowerType.SpreadFire:
                selectedText = "Spread Fire Tower Selected";
                _editButtons[2].GetComponent<Image>().color = new Color(0, 255, 0);
                break;
            case TowerType.AoeFire:
                selectedText = "AOE Fire Tower Selected";
                _editButtons[3].GetComponent<Image>().color = new Color(0, 255, 0);
                break;
            default:
                _priorityButtonGroup.SetActive(false);
                break;
        }

        _editMenu.transform.GetChild(2).GetComponent<Text>().text = selectedText;
    }

    public void SetSelectedPriority(int i)
    {
        AttackChoice priority = AttackChoice.First;

        switch (i)
        {
            case 1:
                priority = AttackChoice.First;
                break;
            case 2:
                priority = AttackChoice.MostHealth;
                break;
            case 3:
                priority = AttackChoice.MostDamage;
                break;
            case 4:
                priority = AttackChoice.Last;
                break;
            case 5:
                priority = AttackChoice.LeastHealth;
                break;
            case 6:
                priority = AttackChoice.LeastDamage;
                break;
            default:
                break;
        }

        UpdateAttackParameters(priority);
        _selectedTower = TowerType.None;
        SetTowerPriorityText();
        SetSelectedTower();
    }

    private void UpdateAttackParameters(AttackChoice option)
    {
        switch (_selectedTower)
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

        _selectedTower = TowerType.None;
        UpdateTowers();
    }
    public void UpdateTowers()
    {

        foreach (GameObject tower in _towers)
        {
            switch (tower.GetComponent<Tower>().GetTowerType())
            {
                case TowerType.SingleFire:
                    tower.GetComponent<Tower>().TargetParameters = _singleFireTarget;
                    break;
                case TowerType.BurstFire:
                    tower.GetComponent<Tower>().TargetParameters = _burstFireTarget;
                    break;
                case TowerType.SpreadFire:
                    tower.GetComponent<Tower>().TargetParameters = _spreadFireTarget;
                    break;
                case TowerType.AoeFire:
                    tower.GetComponent<Tower>().TargetParameters = _aoeTarget;
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
            writer.WriteLine("Single - " + AttackChoiceUtils.GetBaseText(_singleFireTarget));
            writer.WriteLine("Burst - " + AttackChoiceUtils.GetBaseText(_burstFireTarget));
            writer.WriteLine("Spread - " + AttackChoiceUtils.GetBaseText(_spreadFireTarget));
            writer.WriteLine("AOE - " + AttackChoiceUtils.GetBaseText(_aoeTarget));

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
                string attackString = line.Substring(line.LastIndexOf(" ") + 1);//the last word, being the attack choice as a string
                AttackChoice attackChoice = AttackChoice.First;

                foreach (AttackChoice parameter in Enum.GetValues(typeof(AttackChoice)))
                {
                    if (AttackChoiceUtils.GetBaseText(parameter).Equals(attackString))
                    {
                        attackChoice = parameter;
                        break;
                    }
                }

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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditTowerMenu : MonoBehaviour {

    [SerializeField]
    private GameObject _editMenu;

    private TowerSelection _towerSelection;

    private List<GameObject> _towers = new List<GameObject>();

    private bool _on = false;

    void Start()
    {
        _towerSelection = GameObject.Find("TowerSelectUI").GetComponent<TowerSelection>();
    }

    // Update is called once per frame
    void Update () {
		if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleMenu();
        }
        else if (_on)
        {
            UpdateTree();
        }
	}

    private void ToggleMenu()
    {
        _editMenu.transform.GetChild(3).GetComponent<Text>().text = "Selected Tower";
        _editMenu.transform.GetChild(4).GetComponent<Text>().text = "";

        if (!_on)
        {
            this._editMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            this._editMenu.SetActive(false);
            Time.timeScale = 1;
        }

        foreach (Button btn in this._towerSelection._buttons)
        {
            btn.interactable = _on;
        }

        _on = !_on;
    }
    
    private void UpdateTree()
    {
        string selectedTowerText = _editMenu.transform.GetChild(3).GetComponent<Text>().text;

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (selectedTowerText == "Selected Tower" || selectedTowerText == "Black Tower")
            {
                _editMenu.transform.GetChild(3).GetComponent<Text>().text = "Red Tower";
                _editMenu.transform.GetChild(4).GetComponent<Text>().text = "Attack First Enemy (1)  |  Attack Last Enemy (2)  |  Attack Strongest Enemy (3)  |  Attack Weakest Enemy (4)";
            }
            else
            {
                ResetText();
            }
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            if (selectedTowerText == "Selected Tower" || selectedTowerText == "Red Tower")
            {
                _editMenu.transform.GetChild(3).GetComponent<Text>().text = "Black Tower";
                _editMenu.transform.GetChild(4).GetComponent<Text>().text = "Attack First Enemy (1)  |  Attack Last Enemy (2)  |  Attack Strongest Enemy (3)  |  Attack Weakest Enemy (4)";
            }
            else
            {
                ResetText();
            }
        }
        else if (_editMenu.transform.GetChild(4).GetComponent<Text>().text != "")
        {
            string attackingText = "";

            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
            {
                attackingText = "First";
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
            {
                attackingText = "Last";
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
            {
                attackingText = "Strongest";
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
            {
                attackingText = "Weakest";
            }

            if (attackingText != "")
            {
                if (selectedTowerText == "Red Tower")
                {
                    _editMenu.transform.GetChild(5).GetComponent<Text>().text = "Red Tower (R) - Attacking " + attackingText + " Enemy";
                }
                else if (selectedTowerText == "Black Tower")
                {
                    _editMenu.transform.GetChild(6).GetComponent<Text>().text = "Black Tower (B) - Attacking " + attackingText + " Enemy";
                }

                foreach (GameObject tower in _towers)
                {
                    tower.GetComponent<TowerController>().SetAttackOption(attackingText);
                }

                ResetText();
            }

        }

    }

    private void ResetText()
    {
        _editMenu.transform.GetChild(3).GetComponent<Text>().text = "Selected Tower";
        _editMenu.transform.GetChild(4).GetComponent<Text>().text = "";
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

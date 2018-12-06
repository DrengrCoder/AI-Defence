using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class TowerShop : MonoBehaviour {

    [SerializeField]
    private GameObject _towerShopMenu;

    [SerializeField]
    private PlayerController _playerCharacter;
    [SerializeField]
    private CreditBanks _bank;

    [SerializeField]
    private List<GameObject> _statTexts = new List<GameObject>();
    [SerializeField]
    private List<GameObject> _towerPrefabs;
    [SerializeField]
    private List<Button> _upgradeButtons;

    private TowerSelection _towerSelector;

    private bool _menuOn = false;

    void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey)
        {
            if (e.keyCode == KeyCode.U && e.type == EventType.KeyDown)
            {
                ToggleShopMenu();
            }
        }
    }

    // Use this for initialization
    void Start ()
    {
        _towerSelector = GameObject.Find("TowerSelectUI").GetComponent<TowerSelection>();
        ResetPrefabs();
    }

    private void ResetPrefabs()
    {
        foreach (GameObject prefab in _towerPrefabs)
        {
            Tower tower = prefab.GetComponent<Tower>();
            tower.TowerHealth = tower._baseHealth;
            tower.SetTowerDamage(tower.BaseDamage());
            tower.SetTowerFireRate(tower.BaseFireRate());
            tower._upgradeCost = tower._baseUpgradeCost;
            tower._upgradePointer = 0;
            prefab.GetComponent<SphereCollider>().radius = tower.BaseRange();

            int currentCost = 0;
            int costIncrement = 0;
            tower._futureCosts = new int[5];
            for (int i = 0; i < tower._maxUpgrades; i++)
            {
                costIncrement += 100;
                currentCost += costIncrement;

                tower._futureCosts[i] = currentCost;
            }
        }
    }

    public void ToggleShopMenu()
    {
        if (_playerCharacter.gameObject.activeSelf == false && (Time.timeScale == 1 || (Time.timeScale == 0 && _towerShopMenu.activeSelf == true)))
        {
            if (!_menuOn)
            {
                _towerShopMenu.SetActive(true);
                Time.timeScale = 0;
                _menuOn = !_menuOn;
                SetStatText();

                CheckPrices();

                foreach (Button btn in _towerSelector._buttons)
                {
                    btn.interactable = false;
                }
            }
            else
            {
                _towerShopMenu.SetActive(false);
                Time.timeScale = 1;
                _menuOn = !_menuOn;
                _towerSelector.CreditUpdated();
            }

        }
    }

    private void SetStatText()
    {
        StringBuilder damage = new StringBuilder();
        StringBuilder health = new StringBuilder();
        StringBuilder range = new StringBuilder();
        StringBuilder firerate = new StringBuilder();

        damage.Append("DAMAGE");
        health.Append("HEALTH");
        range.Append("RANGE");
        firerate.Append("FIRE RATE");

        foreach (GameObject tower in _towerPrefabs)
        {
            Tower controller = tower.GetComponent<Tower>();

            damage.Append("\n\n").Append(controller.GetTowerDamage().ToString());
            health.Append("\n\n").Append(controller.TowerHealth.ToString());
            range.Append("\n\n").Append(tower.GetComponent<SphereCollider>().radius.ToString());
            firerate.Append("\n\n").Append(controller.GetTowerFireRate().ToString("n2"));
        }

        _statTexts[0].GetComponent<Text>().text = damage.ToString();
        _statTexts[1].GetComponent<Text>().text = health.ToString();
        _statTexts[2].GetComponent<Text>().text = range.ToString();
        _statTexts[3].GetComponent<Text>().text = firerate.ToString();

        for (int i = 0; i < _upgradeButtons.Count; i++)
        {
            string text = _towerPrefabs[i].GetComponent<Tower>()._upgradeCost.ToString() + "QE";
            if (_towerPrefabs[i].GetComponent<Tower>()._upgradeCost < 1)
            {
                text = "MAXED";
            }
            _upgradeButtons[i].GetComponentInChildren<Text>().text = text;
        }
    }
    
    public void BuyUpgrade(GameObject tower)
    {
        GameObject prefab = null;
        int pressedButton = -1;

        switch (tower.GetComponent<Tower>().GetTowerType())
        {
            case TowerType.SingleFire:
                pressedButton = 0;
                break;
            case TowerType.BurstFire:
                pressedButton = 1;
                break;
            case TowerType.SpreadFire:
                pressedButton = 2;
                break;
            case TowerType.AoeFire:
                pressedButton = 3;
                break;
            case TowerType.PulseFire:
                pressedButton = 4;
                break;
            default:
                break;
        }

        if (pressedButton > -1)
        {
            prefab = _towerPrefabs[pressedButton];
        }

        if (prefab != null)
        {
            //baseTower and prefab variables are objects outside of scene

            Tower baseTower = prefab.GetComponent<Tower>();

            _bank.MinusCredits(baseTower._upgradeCost);

            baseTower.TowerHealth = prefab.GetComponent<Tower>().TowerHealth + 50;
            baseTower.SetTowerDamage(baseTower.GetTowerDamage() + 3);

            if (baseTower.GetTowerFireRate() > 0.25f)//keeps the fire rate positive
            {
                baseTower.SetTowerFireRate(baseTower.GetTowerFireRate() - 0.1f);
            }

            prefab.GetComponent<SphereCollider>().radius = prefab.GetComponent<SphereCollider>().radius + 15;
            
            baseTower._upgradeCost = baseTower._futureCosts[++baseTower._upgradePointer];

            foreach (GameObject instancedObject in GameObject.FindGameObjectsWithTag("Tower"))
            {
                //instancedTower and instancedObject are the game object 
                //clones active in the scene hierarchy

                if (instancedObject.name.Contains(prefab.name))
                {
                    Tower instancedTower = instancedObject.GetComponent<Tower>();

                    instancedTower.TowerHealth = baseTower.TowerHealth;
                    instancedTower.SetTowerDamage(baseTower.GetTowerDamage());
                    instancedTower.SetTowerFireRate(baseTower.GetTowerFireRate());
                    instancedTower._upgradeCost = baseTower._upgradeCost;
                    instancedObject.GetComponent<SphereCollider>().radius = prefab.GetComponent<SphereCollider>().radius;
                }
            }

            //check to see if the tower can be upgraded any further
            if (baseTower._upgradePointer + 1 >= baseTower._maxUpgrades)
            {
                _upgradeButtons[pressedButton].GetComponentInChildren<Text>().text = "MAXED";
                baseTower._upgradeCost = 0;
            }

            CheckPrices();
        }
        
        SetStatText();
    }

    private void CheckPrices()
    {
        for (int i = 0; i < _towerPrefabs.Count; i++)
        {
            Tower t = _towerPrefabs[i].GetComponent<Tower>();
            if (t._upgradeCost <= _bank.CreditBank && t._upgradeCost > 0)
            {
                _upgradeButtons[i].interactable = true;
            }
            else
            {
                _upgradeButtons[i].interactable = false;
            }
        }
    }
}

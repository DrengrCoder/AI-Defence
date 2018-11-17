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
    private List<GameObject> _statTexts = new List<GameObject>();

    [SerializeField]
    private List<GameObject> _towerPrefabs = new List<GameObject>();

    [SerializeField]
    private List<GameObject> _upgradeButtons = new List<GameObject>();

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
            prefab.GetComponent<SphereCollider>().radius = tower.BaseRange();
        }
    }

    private void ToggleShopMenu()
    {
        if (_playerCharacter.gameObject.activeSelf == false && (Time.timeScale == 1 || (Time.timeScale == 0 && _towerShopMenu.activeSelf == true)))
        {
            if (!_menuOn)
            {
                _towerShopMenu.SetActive(true);
                Time.timeScale = 0;
                _menuOn = !_menuOn;
                SetStatText();
            }
            else
            {
                _towerShopMenu.SetActive(false);
                Time.timeScale = 1;
                _menuOn = !_menuOn;
            }

            foreach (Button btn in _towerSelector._buttons)
            {
                btn.interactable = !_menuOn;
            }
        }
    }

    private void SetStatText()
    {
        StringBuilder damage = new StringBuilder();
        StringBuilder health = new StringBuilder();
        StringBuilder range = new StringBuilder();
        StringBuilder firerate = new StringBuilder();

        damage.Append("\nDamage\n");
        health.Append("\nHealth\n");
        range.Append("\nRange\n");
        firerate.Append("\nFire Rate\n");

        foreach (GameObject tower in _towerPrefabs)
        {
            Tower controller = tower.GetComponent<Tower>();

            damage.Append("\n\n\n").Append(controller.GetTowerDamage().ToString());
            health.Append("\n\n\n").Append(controller.TowerHealth.ToString());
            range.Append("\n\n\n").Append(tower.GetComponent<SphereCollider>().radius.ToString());
            firerate.Append("\n\n\n").Append(controller.GetTowerFireRate().ToString("n2"));
        }

        _statTexts[0].GetComponent<Text>().text = damage.ToString();
        _statTexts[1].GetComponent<Text>().text = health.ToString();
        _statTexts[2].GetComponent<Text>().text = range.ToString();
        _statTexts[3].GetComponent<Text>().text = firerate.ToString();
    }
    
    public void BuyUpgrade(GameObject tower)
    {
        GameObject prefab = null;

        switch (tower.GetComponent<Tower>().GetTowerType())
        {
            case TowerType.SingleFire:
                prefab = _towerPrefabs[0];
                break;
            case TowerType.BurstFire:
                prefab = _towerPrefabs[1];
                break;
            case TowerType.SpreadFire:
                prefab = _towerPrefabs[2];
                break;
            case TowerType.AoeFire:
                prefab = _towerPrefabs[3];
                break;
            case TowerType.PulseFire:
                prefab = _towerPrefabs[4];
                break;
            default:
                break;
        }

        if (prefab != null)
        {
            //baseTower and prefab variables are objects outside of scene

            Tower baseTower = prefab.GetComponent<Tower>();

            baseTower.TowerHealth = prefab.GetComponent<Tower>().TowerHealth + 50;
            baseTower.SetTowerDamage(baseTower.GetTowerDamage() + 3);

            if (baseTower.GetTowerFireRate() > 0.25f)//keeps the fire rate positive
            {
                baseTower.SetTowerFireRate(baseTower.GetTowerFireRate() - 0.1f);
            }

            prefab.GetComponent<SphereCollider>().radius = prefab.GetComponent<SphereCollider>().radius + 15;

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
                    instancedObject.GetComponent<SphereCollider>().radius = prefab.GetComponent<SphereCollider>().radius;
                }
            }
        }
        
        SetStatText();
    }
}

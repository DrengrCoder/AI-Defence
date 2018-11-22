using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateObjectOnclick : MonoBehaviour {

    private Ray _ray;
    private RaycastHit _hit;

    [SerializeField]
    private int _num = 0;

    [SerializeField]
    private GameObject _aoeTower;
    [SerializeField]
    private GameObject _singleFireTower;
    [SerializeField]
    private GameObject _burstFireTower;
    [SerializeField]
    private GameObject _spreadFireTower;
    [SerializeField]
    private GameObject _pulseFireTower;

    private TowerSelection _towerSelection;

    private GameObject _thisSpot;

	// Use this for initialization
	void Start () {
		this._towerSelection = GameObject.Find("TowerSelectUI").GetComponent<TowerSelection>();
        this._thisSpot = this.gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (Physics.Raycast(_ray, out _hit))
            {
                if (this._hit.transform.name == this._thisSpot.name && TowerIsSelected())
                {
                    GameObject obj = null;

                    if (this._towerSelection.AoeTowerSelected())
                    {
                        obj = Instantiate(_aoeTower, SpawnPosition(), Quaternion.identity) as GameObject;
                    }
                    else if (this._towerSelection.SingleFireTowerSelected())
                    {
                        obj = Instantiate(_singleFireTower, SpawnPosition(), Quaternion.identity) as GameObject;
                    }
                    else if (this._towerSelection.BurstFireTowerSelected())
                    {
                        obj = Instantiate(_burstFireTower, SpawnPosition(), Quaternion.identity) as GameObject;
                    }
                    else if (this._towerSelection.PulseTowerSelected())
                    {
                        obj = Instantiate(_pulseFireTower, SpawnPosition(), Quaternion.identity) as GameObject;
                    }
                    else if (this._towerSelection.SpreadTowerSelected())
                    {
                        obj = Instantiate(_spreadFireTower, SpawnPosition(), Quaternion.identity) as GameObject;
                    }

                    if (obj.GetComponent<Tower>())
                    {
                        obj.GetComponent<Tower>().Num = _num;
                    }

                    this._towerSelection.ResetButtons();
                }
            }
        }
    }

    private Vector3 SpawnPosition()
    {
        return new Vector3(this._thisSpot.transform.position.x, this._thisSpot.transform.position.y, this._thisSpot.transform.position.z);
    }

    private bool TowerIsSelected()
    {
        return this._towerSelection.AoeTowerSelected() || 
            this._towerSelection.SingleFireTowerSelected() || 
            this._towerSelection.BurstFireTowerSelected() ||
            this._towerSelection.PulseTowerSelected() ||
            this._towerSelection.SpreadTowerSelected();
    }

}

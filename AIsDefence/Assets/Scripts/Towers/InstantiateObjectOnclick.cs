using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateObjectOnclick : MonoBehaviour {

    private Ray _ray;
    private RaycastHit _hit;

    [SerializeField]
    private int _num = 0;

    //[SerializeField]
    //private GameObject _aoeTower;
    //[SerializeField]
    //private GameObject _singleFireTower;
    //[SerializeField]
    //private GameObject _burstFireTower;
    //[SerializeField]
    //private GameObject _spreadFireTower;
    //[SerializeField]
    //private GameObject _pulseFireTower;
    
    private GameObject _thisSpot;


    [HideInInspector]
    public bool MenuActiveOverThis = false;


	// Use this for initialization
	void Start () {
        this._thisSpot = this.gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        //_ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //if (Input.GetKey(KeyCode.Mouse0))
        //{
        //    if (Physics.Raycast(_ray, out _hit))
        //    {
        //        if (this._hit.transform.name == this._thisSpot.name && TowerIsSelected())
        //        {
        //            if (this._towerSelection.AoeTowerSelected())
        //            {
        //                SpawnObject(_aoeTower);
        //            }
        //            else if (this._towerSelection.SingleFireTowerSelected())
        //            {
        //                SpawnObject(_singleFireTower);
        //            }
        //            else if (this._towerSelection.BurstFireTowerSelected())
        //            {
        //                SpawnObject(_burstFireTower);
        //            }
        //            else if (this._towerSelection.PulseTowerSelected())
        //            {
        //                SpawnObject(_pulseFireTower);
        //            }
        //            else if (this._towerSelection.SpreadTowerSelected())
        //            {
        //                SpawnObject(_spreadFireTower);
        //            }
                    
        //            this._towerSelection.ResetButtons();
        //        }
        //    }
        //}
    }


    public void SpawnObject(GameObject obj)
    {
        GameObject tower = Instantiate(obj, SpawnPosition(), Quaternion.identity) as GameObject;

        if (tower.GetComponent<Tower>())
        {
            tower.GetComponent<Tower>().Num = _num;
        }
    }


    private Vector3 SpawnPosition()
    {
        return new Vector3(this._thisSpot.transform.position.x, this._thisSpot.transform.position.y, this._thisSpot.transform.position.z);
    }
    
}

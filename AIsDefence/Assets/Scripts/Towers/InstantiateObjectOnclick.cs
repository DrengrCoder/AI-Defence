using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateObjectOnclick : MonoBehaviour {

    private Ray _ray;
    private RaycastHit _hit;

    [SerializeField]
    private GameObject _redPrefab;
    [SerializeField]
    private GameObject _blackPrefab;

    private bool _pressedDelay = false;

    private TowerSelection _towerSelection;

    private GameObject _thisSpot;

	// Use this for initialization
	void Start () {
		this._towerSelection = GameObject.Find("Canvas").GetComponent<TowerSelection>();
        this._thisSpot = this.gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (Physics.Raycast(_ray, out _hit))
            {
                if (this._hit.transform.name == this._thisSpot.name && (this._towerSelection.RedTowerSelected() || this._towerSelection.BlackTowerSelected()))
                {
                    if (this._towerSelection.RedTowerSelected())
                    {
                        GameObject obj = Instantiate(_redPrefab, SpawnPosition(), Quaternion.identity) as GameObject;
                    }
                    else if (this._towerSelection.BlackTowerSelected())
                    {
                        GameObject obj = Instantiate(_blackPrefab, SpawnPosition(), Quaternion.identity) as GameObject;
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

}

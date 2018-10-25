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

	// Use this for initialization
	void Start () {
		this._towerSelection = GameObject.Find("Canvas").GetComponent<TowerSelection>();
    }
	
	// Update is called once per frame
	void Update () {
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray, out _hit))
        {
            if (this._towerSelection.RedTowerSelected() || this._towerSelection.BlackTowerSelected()) {
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    if (this._towerSelection.RedTowerSelected())
                    {
                        GameObject obj = Instantiate(_redPrefab, new Vector3(_hit.point.x, _hit.point.y, _hit.point.z), Quaternion.identity) as GameObject;
                    }
                    else if (this._towerSelection.BlackTowerSelected())
                    {
                        GameObject obj = Instantiate(_blackPrefab, new Vector3(_hit.point.x, _hit.point.y, _hit.point.z), Quaternion.identity) as GameObject;
                    }

                    this._towerSelection.ResetButtons();
                }
            }
        }
    }

}

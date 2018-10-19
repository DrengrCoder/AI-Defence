using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateObjectOnclick : MonoBehaviour {

    private Ray ray;
    private RaycastHit hit;

    public GameObject redPrefab;
    public GameObject blackPrefab;

    private bool pressedDelay = false;

    private TowerSelection towerSelection;

	// Use this for initialization
	void Start () {
		this.towerSelection = GameObject.Find("Canvas").GetComponent<TowerSelection>();
        Debug.Log(this.towerSelection == null);
    }
	
	// Update is called once per frame
	void Update () {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (this.pressedDelay == false && (this.towerSelection.RedTowerSelected() || this.towerSelection.BlackTowerSelected())) {
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    pressedDelay = true;

                    if (this.towerSelection.RedTowerSelected())
                    {
                        GameObject obj = Instantiate(redPrefab, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity) as GameObject;
                    }
                    else if (this.towerSelection.BlackTowerSelected())
                    {
                        GameObject obj = Instantiate(blackPrefab, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity) as GameObject;
                    }

                    StartCoroutine(ResetDelay());
                }
            }

        }
    }

    IEnumerator ResetDelay()
    {
        yield return new WaitForSeconds(1);
        pressedDelay = false;
    }
}

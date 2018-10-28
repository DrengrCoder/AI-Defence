using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditTowerMenu : MonoBehaviour {

    [SerializeField]
    private GameObject _editMenu;

    private bool _on = false;
    	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleMenu();
        }
	}

    private void ToggleMenu()
    {
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

        _on = !_on;
    }
}

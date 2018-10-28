using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        _on = !_on;
    }
    
    private void UpdateTree()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (_editMenu.transform.GetChild(3).GetComponent<Text>().text == "Selected Tower" ||
                _editMenu.transform.GetChild(3).GetComponent<Text>().text == "Black Tower")
            {
                _editMenu.transform.GetChild(3).GetComponent<Text>().text = "Red Tower";
                _editMenu.transform.GetChild(4).GetComponent<Text>().text = "text";
            }
            else
            {
                _editMenu.transform.GetChild(3).GetComponent<Text>().text = "Selected Tower";
                _editMenu.transform.GetChild(4).GetComponent<Text>().text = "";
            }
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            if (_editMenu.transform.GetChild(3).GetComponent<Text>().text == "Selected Tower" ||
                _editMenu.transform.GetChild(3).GetComponent<Text>().text == "Red Tower")
            {
                _editMenu.transform.GetChild(3).GetComponent<Text>().text = "Black Tower";
                _editMenu.transform.GetChild(4).GetComponent<Text>().text = "text";
            }
            else
            {
                _editMenu.transform.GetChild(3).GetComponent<Text>().text = "Selected Tower";
                _editMenu.transform.GetChild(4).GetComponent<Text>().text = "";
            }
        }
    }
}

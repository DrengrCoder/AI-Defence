using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseMenu : MonoBehaviour {

	public GameObject ESCMenu;

    private bool _on = false;

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            SwitchState();
        }
    }

    public void SwitchState()
    {
        if (_on == true)
        {
            ESCMenu.SetActive(true);
            Time.timeScale = 0;
            _on = false;
        }
        else
        {
            ESCMenu.SetActive(false);
            Time.timeScale = 1;
            _on = true;
        }
    }

    public void Exit()
    {
        Debug.Log("Exit Button Has Been Pressed!");
    }

}

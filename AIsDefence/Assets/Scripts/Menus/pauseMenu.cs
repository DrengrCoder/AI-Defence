using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour {

	public GameObject ESCMenu;

    [SerializeField]
    private PlayerController _controller;

    private bool _on = true;

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            SwitchState();
        }
    }

    public void SwitchState()
    {
        if ((Time.timeScale == 0 && ESCMenu.activeSelf == true) || (Time.timeScale == 1))
        {
            if (_on == true)
            {
                ESCMenu.SetActive(true);
                Time.timeScale = 0;
                _on = false;
                _controller.Pause = true;
            }
            else
            {
                ESCMenu.SetActive(false);
                Time.timeScale = 1;
                _on = true;
                _controller.Pause = false;
            }
        }
    }

    public void Exit()
    {
        SceneManager.LoadScene("StartMenu", LoadSceneMode.Single);
    }

}

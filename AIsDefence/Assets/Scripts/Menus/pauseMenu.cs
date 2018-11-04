using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseMenu : MonoBehaviour {

	public GameObject ESCMenu;

    [SerializeField]
    private PlayerController _controller;

    private bool _on = false;
    private float _time;

    private void Start()
    {
        _time = Time.fixedDeltaTime;
    }

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
            _controller.Pause = true;
        }
        else
        {
            ESCMenu.SetActive(false);
            Time.timeScale = 1;
            Time.fixedDeltaTime = _time;
            _on = true;
            _controller.Pause = false;
        }
    }

    public void Exit()
    {
        Debug.Log("Exit Button Has Been Pressed!");
    }

}

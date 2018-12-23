using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour {

	public GameObject ESCMenu;

    [SerializeField]
    private PlayerController _controller;
    [SerializeField]
    private AudioSource _Sound;

    [SerializeField]
    private RadialMenuController _rmc;

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
                Cursor.visible = true;
                _rmc.PauseWheel(true);
            }
            else
            {
                ESCMenu.SetActive(false);
                Time.timeScale = 1;
                _on = true;
                _controller.Pause = false;

                if (_controller.gameObject.activeSelf == true)
                {
                    Cursor.visible = false;
                }

                _rmc.PauseWheel(false);
            }
        }
    }

    public void Exit()
    {
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        Time.timeScale = 1;
        _Sound.Play();

        yield return new WaitForSeconds(0.3f);

        SceneManager.LoadScene("StartMenu", LoadSceneMode.Single);
    }

}

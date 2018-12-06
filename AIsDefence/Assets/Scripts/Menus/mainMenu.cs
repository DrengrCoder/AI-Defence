using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour {

    [SerializeField]
    private GameObject _Main;
    [SerializeField]
    private GameObject _Level;

    public void ToScene(string scene)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    public void ToLevelSelect()
    {
        _Level.GetComponent<Levels>().ActivateButtons();
        _Level.SetActive(true);
        _Main.SetActive(false);
    }

    public void Back()
    {
        _Level.SetActive(false);
        _Main.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

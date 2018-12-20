using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour {

    [SerializeField]
    private GameObject _Main;
    [SerializeField]
    private GameObject _Level;
    [SerializeField]
    private AudioSource _Sound;

    public void ToScene(string scene)
    {
        StartCoroutine(Delay(scene));
    }

    private IEnumerator Delay(string scene)
    {
        _Sound.Play();

        yield return new WaitForSeconds(0.3f);

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
        StartCoroutine(DelayQuit());
    }

    private IEnumerator DelayQuit()
    {
        _Sound.Play();

        yield return new WaitForSeconds(0.3f);

        Application.Quit();
    }
}

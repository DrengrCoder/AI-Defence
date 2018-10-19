using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour {

    public string TestScene;

    public void StartGame()
    {
        SceneManager.LoadScene(TestScene, LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Button Has Been Pressed!");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameLevelSelect : MonoBehaviour {

    public void ToScene(string scene)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    public void Restart()
    {
        //This is so in the future instead of recalling the scene
        //the level parameters will just reset
    }
}

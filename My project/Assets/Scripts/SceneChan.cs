using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChan : MonoBehaviour
{
    public bool firstTime = true;

    private void Awake() {
        firstTime = intToBool(PlayerPrefs.GetInt("First"));
    }

    public void StartGame()
    {
        Invoke("LoadGameScene", 0.3f); // Calls LoadGameScene after 2 seconds
    }

    public void CloseGame()
    {
        Invoke("QuitApplication", 0.3f); // Calls QuitApplication after 2 seconds
    }

    private void LoadGameScene()
    {
        if (firstTime){
            PlayerPrefs.SetInt("First", 0); 
            SceneManager.LoadScene("Scenes/poczatek");
        }
        else SceneManager.LoadScene("Scenes/TheGame");
    }

    private void QuitApplication()
    {
        Application.Quit();
    }

    private bool intToBool(int i) {
        if (i == 0) return false;
        else return true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using System.Threading.Thread;

/*
public class SceneChan : MonoBehaviour
{
    public async void StartGame() {
        await Task.Delay(2000);
        SceneManager.LoadScene("Scenes/TheGame");
    }

    public async void CloseGame() {
        await Task.Delay(2000);
        Application.Quit();
    }
}
*/
public class SceneChan : MonoBehaviour
{
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
        SceneManager.LoadScene("Scenes/TheGame");
    }

    private void QuitApplication()
    {
        Application.Quit();
    }
}

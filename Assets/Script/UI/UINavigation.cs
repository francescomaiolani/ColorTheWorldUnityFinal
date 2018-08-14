using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UINavigation : MonoBehaviour {

    public string sceneToChange;

    public GameObject mask, orangePanel;

    public void ChangeScene()
    {
        SceneManager.LoadSceneAsync(sceneToChange);
    }

    public void SetSceneToChange(string sceneName) {
        if (mask != null && orangePanel != null)
        {
            sceneToChange = sceneName;
            mask.SetActive(true);
            orangePanel.SetActive(true);
            Invoke("ChangeScene", 1.1f);
        }

        else
            ChangeScene();
       
    }

    /*public void GoToHome() {
        SceneManager.LoadSceneAsync("Home");
    }

    public void GoToShop()  {
        SceneManager.LoadSceneAsync("Shop");
    }

    public void GoToGame() {
        SceneManager.LoadSceneAsync("Game");
    }

    public void GoToMenu()
    {
        SceneManager.LoadSceneAsync("Menu");
    }*/
}

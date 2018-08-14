using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UINavigation : MonoBehaviour {

    public void GoToHome() {
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
    }
}

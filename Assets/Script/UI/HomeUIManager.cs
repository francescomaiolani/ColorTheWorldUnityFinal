using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HomeUIManager : MonoBehaviour {

    public Text goldText, gemsText;
    GameController gameController;

    public delegate void OnShadowToChange();
    public static event OnShadowToChange ChangeShadow;

    void Start () {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        GameController.ChangedStats += UpdateText;

        UpdateText();
	}

    void UpdateText() {
        goldText.text = gameController.resourceManager.FindResource("gold").GetAmount().ToString();
        gemsText.text = gameController.resourceManager.FindResource("gems").GetAmount().ToString();

        //ChangeShadow();
    }

    private void OnDisable()
    {
        GameController.ChangedStats -= UpdateText;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HomeUIManager : MonoBehaviour {

    public Text goldText, gemsText, levelText;
    GameController gameController;

    public delegate void OnShadowToChange();
    public static event OnShadowToChange ChangeShadow;

    void Start () {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        GameController.ChangedStats += UpdateText;

        UpdateText();
	}

    void UpdateText() {
        goldText.text = ConvertCostToString(gameController.resourceManager.FindResource("gold").GetAmount());
        gemsText.text = ConvertCostToString(gameController.resourceManager.FindResource("gems").GetAmount());
        levelText.text = gameController.resourceManager.FindResource("level").GetAmount().ToString();
        //ChangeShadow();
    }

    public static string ConvertCostToString(int cost)
    {
        string text = cost.ToString();
        if (cost < 1000000 && cost >= 1000)
            text = text.Substring(0, text.Length - 3) + "." + text.Substring(text.Length - 3);
        return text;
    }

    private void OnDisable()
    {
        GameController.ChangedStats -= UpdateText;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour {

    public Text goldText, gemsText;
    GameController gameController;
    public Slider specialPointBar;

    public float specialPoint;

    public delegate void OnShadowToChange();
    public static event OnShadowToChange ChangeShadow;

    void Start () {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        GameController.ChangedStats += UpdateText;
        EnemyVariable.EnemyDeadSpecialPoint += AddSpecialPoint;

        specialPoint = 0;
        UpdateText();
    }

    void UpdateText() {
        goldText.text = gameController.resourceManager.FindResource("gold").GetAmount().ToString();
        gemsText.text = gameController.resourceManager.FindResource("gems").GetAmount().ToString();
        //ChangeShadow();
    }

    public void AddSpecialPoint(float amount) {
        specialPoint += amount;
        specialPoint = Mathf.Clamp(specialPoint, 0, 100);

        specialPointBar.value = specialPoint;
    }

    private void OnDisable() {
        GameController.ChangedStats -= UpdateText;
        EnemyVariable.EnemyDeadSpecialPoint -= AddSpecialPoint;

        gameController.SaveAllData();

    }


}


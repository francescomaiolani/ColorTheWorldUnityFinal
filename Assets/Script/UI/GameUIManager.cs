using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour {

    public Text goldText;
    GameController gameController;
    public Slider specialPointBar;

    public Camera mainCamera;
    float xConversionNumberToCanvas;
    float yConversionNumberToCanvas;

    public float specialPoint;

    public GameObject goldTextPrefab;
    public GameObject lifeTextPrefab;
    public GameObject specialTextPrefab;

    public Transform canvasTransform;

    public delegate void OnShadowToChange();
    public static event OnShadowToChange ChangeShadow;

    void Start () {
        float ySize = mainCamera.orthographicSize;
        float xSize = mainCamera.orthographicSize * mainCamera.aspect;
        float screenWidth = Screen.width/2;
        float screenHeight = Screen.height/2;
        xConversionNumberToCanvas = screenWidth / xSize;
        yConversionNumberToCanvas = screenHeight / ySize;

        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        GameController.ChangedStats += UpdateText;
        EnemyVariable.EnemyDeadSpecialPoint += AddSpecialPoint;
        EnemyVariable.EnemyDeadSpawnResourceText += SpawnResourceText;

        specialPoint = 0;
        UpdateText();
    }

    void UpdateText() {
        goldText.text = HomeUIManager.ConvertCostToString(gameController.resourceManager.FindResource("gold").GetAmount());
        goldText.GetComponent<Animator>().SetTrigger("grow");
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

    void SpawnResourceText(int amount, Vector2 position, string type) {

        float offset = 20;
        GameObject text = null;

        if (type == "gold")
            text = Instantiate(goldTextPrefab, canvasTransform, false);
        else if (type == "special")
            text = Instantiate(specialTextPrefab, canvasTransform, false);
        else if (type == "life")
            text = Instantiate(lifeTextPrefab, canvasTransform, false);

        text.transform.localScale = new Vector3(1, 1, 1);
        Vector3 newPosition = new Vector3(position.x * xConversionNumberToCanvas + offset , position.y * yConversionNumberToCanvas);
        text.transform.localPosition = newPosition;
        text.GetComponentInChildren<Text>().text = "+ " + amount.ToString();
        
        Destroy(text.gameObject, 1f);
    }


}


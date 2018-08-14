using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameWall : MonoBehaviour {

    public float maxLife;
    public float actualLife;
    public Slider wallLife;

    public Wall wall;

    private void Start()
    {
        EnemyVariable.ApplyWallDamage += ApplyDamage;
        wallLife.onValueChanged.AddListener(delegate { WallLifeChanged(); });

        AssignWall();
     
    }

    void WallLifeChanged() {
        if (wallLife.value < actualLife)
            Invoke("IncreaseWallLife", 0.05f);
        else if (wallLife.value > actualLife)
            Invoke("DecreaseWallLife", 0.05f);
    }

    private void DecreaseWallLife() {
        wallLife.value -= 1;
    }
    private void IncreaseWallLife() {
        wallLife.value += 1;
    }

    void AssignWall() {
        wall = new Wall("Normal Barrel", "", 100, 0);
        maxLife = wall.maxLife;
        actualLife = maxLife;
        wallLife.maxValue = maxLife;
    }


    public void ApplyDamage(float damage) {
        actualLife -= damage;
        actualLife = Mathf.Clamp(actualLife, 0, maxLife);
        wallLife.value = wallLife.value-1;
        if (actualLife <= 0) {
            WallDestroyed();
        }
    }

    public void WallDestroyed() {
        SceneManager.LoadSceneAsync("Home");
    }

    private void OnDisable()
    {
        EnemyVariable.ApplyWallDamage -= ApplyDamage;
    }
}

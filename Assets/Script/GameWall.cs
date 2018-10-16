using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameWall : MonoBehaviour {

    float maxLife;
    float actualLife;

    public Wall wall;

    private void Start()
    {
        EnemyVariable.ApplyWallDamage += ApplyDamage;
        AssignWall();
     
    }

    void AssignWall() {
        wall = new Wall("Normal Barrel", "", 100, 0);
        maxLife = wall.maxLife;
        actualLife = maxLife;
    }


    public void ApplyDamage(float damage) {
        actualLife -= damage;
        actualLife = Mathf.Clamp(actualLife, 0, maxLife);
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

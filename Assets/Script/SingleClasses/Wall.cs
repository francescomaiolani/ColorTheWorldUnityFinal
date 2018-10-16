using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall {

    public string name;
    public string description;
    public float maxLife;
    public bool damageAtAttack;
    public float damageAmount;

    public bool acquired;
    public int level;
    public int levelToAcquire;
    public int goldCost;
    public float maxLifeIndicator;

    public Wall(string name, string description, float maxLife, float damageAmount) {
        this.name = name;
        this.description = description;
        this.maxLife = maxLife;
        this.damageAmount = damageAmount;

        if (damageAmount > 0)
            damageAtAttack = true;
    }

    public void SetShopWallStats(int levelToAcquire, int goldCost, int maxLifeIndicator) {
        this.levelToAcquire = levelToAcquire;
        this.goldCost = goldCost;
        this.maxLifeIndicator = maxLifeIndicator;
    }

}

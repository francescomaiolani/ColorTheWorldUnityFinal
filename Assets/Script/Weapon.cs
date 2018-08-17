using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon {

    public string name;
    public string description;
    public float damagexBullet;
    public float fireRate;
    public float reloadTick;
    public float heatingTick;
    public int bulletCount;
    public int perforante;
    public float bulletSpread;

    public bool acquired;
    public int level;
    public int levelToAcquire;
    public int goldCost;
    public float damageIndicator;
    public float fireRateIndicator;
    public float reloadRateIndicator;
    public float heatingRateIndicator;

    public int cardNumber;
    public int cardRarity;

    public Weapon(string name, string description, float damagexBullet, float fireRate, float reloadTick, float heatingTick, int bulletCount, int perforante, float bulletSpread)
    {
        this.name = name;
        this.description = description;
        this.damagexBullet = damagexBullet;
        this.fireRate = fireRate;
        this.reloadTick = reloadTick;
        this.heatingTick = heatingTick;
        this.bulletCount = bulletCount;
        this.perforante = perforante;
        this.bulletSpread = bulletSpread;
    }

    public void SetShopWeaponStats(int levelToAcquire, int goldCost, float damageIndicator, float fireRateIndicator, float reloadRateIndicator, float heatingRateIndicator) {
        this.levelToAcquire = levelToAcquire;
        this.goldCost = goldCost;
        this.damageIndicator = damageIndicator;
        this.fireRateIndicator = fireRateIndicator;
        this.reloadRateIndicator = reloadRateIndicator;
        this.heatingRateIndicator = heatingRateIndicator;
    }

    public void SetLevel(int newLevel)
    {
        level = newLevel;
    }

    public void SetAcquired(bool acquistata) {
        acquired = acquistata;
    }
}

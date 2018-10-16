using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baloon {

    string name;
    float mass;
    string range;
    float maxSpeed;
    float damage;
    float impactImpulse;
    int numberOfBaloons;
    float areaRange;
    float effectDuration;

    bool acquired;
    int goldCost;
    int cardNumber;
    int levelToAcquire;
    int level;
    Debuf effect;

    //normal baloon no buff
    public Baloon(string name, float mass, string range,  float damage, float impactImpulse,int numberOfBaloons ) {
        this.name = name;
        this.mass = mass;
        this.range = range;
        this.damage = damage;
        this.impactImpulse = impactImpulse;
        this.numberOfBaloons = numberOfBaloons;
        SetMaxSpeed();

    }

    //normal baloon but with explosion
    public Baloon(string name, float mass, string range,  float damage, float impactImpulse, int numberOfBaloons, float areaRange)
    {
        this.name = name;
        this.mass = mass;
        this.range = range;
        this.damage = damage;
        this.impactImpulse = impactImpulse;
        this.numberOfBaloons = numberOfBaloons;
        this.areaRange = areaRange;
        SetMaxSpeed();

    }

    //normal baloon but with explosion
    public Baloon(string name, float mass, string range, float damage, float impactImpulse, int numberOfBaloons, float areaRange, Debuf effect, float effectDuration)
    {
        this.name = name;
        this.mass = mass;
        this.range = range;
        this.damage = damage;
        this.impactImpulse = impactImpulse;
        this.numberOfBaloons = numberOfBaloons;
        this.areaRange = areaRange;
        this.effect = effect;
        this.effectDuration = effectDuration;
        SetMaxSpeed();
    }
    void SetMaxSpeed() {
        if (range == "little")
            maxSpeed = 10;
        else if (range == "medium")
            maxSpeed = 20;
        else if (range == "long")
            maxSpeed = 30;
    }

    public void SetAcquired(bool value) {
        acquired = value;
    }
    public void SetLevel(int amount) {
        level = amount;
    }
    public void SetCardNumber(int amount) {
        cardNumber = amount;
    }

    public float GetMaxSpeed() {
        return maxSpeed;
    }
    public float GetDamage()
    {
        return damage;
    }
    public float GetImpactImpulse()
    {
        return impactImpulse;
    }
    public string GetName() {
        return name;
    }
    public bool GetAcquired()
    {
        return acquired;
    }
    public int GetCardNumber() {
        return cardNumber;
    }
    public int GetLevel() {
        return level;
    }

    public int GetLevelToAcquire() {
        return levelToAcquire;
    }
    public int GetGoldCost() {
        return goldCost;
    }

    public void AddToCardNumber(int amount) {
        cardNumber += amount;
    }
}

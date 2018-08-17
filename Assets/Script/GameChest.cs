using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameChest : MonoBehaviour {

    public delegate void OnGoldenChestTouched(int amount);
    public static event OnGoldenChestTouched GiveGold;
    public delegate void OnLifeChestTouched(int amount);
    public static event OnLifeChestTouched GiveLife;
    public delegate void OnSpecialChestTouched(float amount);
    public static event OnSpecialChestTouched GiveSpecialPoint;

    public int minGoldValue;
    public int maxGoldValue;
    public int minBigGoldValue;
    public int maxBigGoldValue;
    public int minLifeValue;
    public int maxLifeValue;
    public int minSpecialValue;
    public int maxSpecialValue;
    public GameObject chestContainer;
    public SpriteRenderer sprite;

    string chestType;

    private void Start()
    {
        sprite.sprite = Resources.Load<Sprite>("ShopButton/WeaponCardTypeOrangeIcon");
    }

    public void AssignChestType(string tipo) {
        if (tipo == "gold")
            chestType = "gold";
        if (tipo == "bigGold")
            chestType = "bigGold";
        else if (tipo == "life")
            chestType = "life";
        else if (tipo == "special")
            chestType = "special";
    }

    private void OnMouseEnter()
    {
        if (chestType == "gold") {
            int goldAmount = Random.Range(minGoldValue, maxGoldValue);
            GiveGold(goldAmount);
        }

        else if (chestType == "gold")
        {
            int goldAmount = Random.Range(minBigGoldValue, maxBigGoldValue);
            GiveGold(goldAmount);
        }

        else if (chestType == "life") {
            int lifeamount = Random.Range(minLifeValue, maxLifeValue);
            GiveLife(lifeamount);
        }

        else if (chestType == "special") {
            float specialAmount = Random.Range(minSpecialValue, maxSpecialValue);
            GiveSpecialPoint(specialAmount);
        }

        Destroy(chestContainer);
    }
}

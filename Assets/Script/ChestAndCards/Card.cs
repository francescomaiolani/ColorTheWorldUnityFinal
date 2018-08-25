using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card: MonoBehaviour {

    string title;
    //weapon, wall, special
    string type;
    bool special;
    int amount;

    public Text cardTitle;
    public Image background;
    public Image greyArea;
    public Image cardType;
    public Text cardAmount;


    public void SetCardStats(string title, string type, bool special, int amount) {
        this.title = title;
        this.type = type;
        this.special = special;
        this.amount = amount;

        AssignCardValues();
    }

    void AssignCardValues() {
        cardTitle.text = title;
        cardAmount.text = "x" + amount.ToString();   
    }

    public string GetTitle() {
        return title;
    }

    public string GetCardType() {
        return type;
    }

    public int GetAmount() {
        return amount;
    }

}

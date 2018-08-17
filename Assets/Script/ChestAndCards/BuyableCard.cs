using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BuyableCard: MonoBehaviour {

    bool special;
    //Weapon, Wall, Special
    string type;
    //card name
    string title;
    //based on name
    string imagePath;
    //quantity of cards received
    int amount;
    //gold or gems
    string costType;
    //price
    int cost;

    public Sprite specialBackground;
    public Sprite rays;
    public Image background;
    public Image greyBackground;
    public Image raysImage;
    public Text nameText;
    public Image cardTypeImage;
    public Image cardImage;
    public Text amountText;
    public Text costText;
    public Image costImage;

    public void SetCardStats(string type, string title, string imagePath,int amount,  string costType, int cost, bool special)
    {
        this.type = type;
        this.title = title;
        this.imagePath = imagePath;
        this.amount = amount;
        this.costType = costType;
        this.cost = cost;
        this.special = special;

        AssignCardStats();
    }

    void AssignCardStats() {
        if (special)
        {
            background.overrideSprite = specialBackground;
            raysImage.overrideSprite = rays;
            greyBackground.color = new Color32(255,211,136,255);
            //background.rectTransform.sizeDelta = new Vector2(600,740);
            cardTypeImage.sprite = Resources.Load<Sprite>("BuyableCard/" + type + "CardTypeOrangeIcon");
        }
        else {
            cardTypeImage.sprite = Resources.Load<Sprite>("BuyableCard/" + type + "CardTypeIcon");
        }

        nameText.text = title.ToUpper();
        //ANCORA DA ASSEGNARE QUESTO
        cardImage.sprite = Resources.Load<Sprite>(imagePath);
        amountText.text = "x" + amount.ToString();
        costText.text = cost.ToString();
        costImage.sprite = Resources.Load<Sprite>("BuyableCard/" + costType);

        SetNativeSizeImage();
    }

    public void SetNativeSizeImage() {
        cardImage.SetNativeSize();

    }




}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


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
    public Sprite specialTitleBackground;

    public Sprite rays;
    public Image background;
    public Image greyBackground;
    public Image titleBackground;
    public Image raysImage;
    public Text nameText;
    public Image cardTypeImage;
    public Image cardImage;
    public Text amountText;
    public Text costText;
    public Image costImage;
    public Button buyCardButton;

    public void SetCardStats(string type, string title, string imagePath,int amount,  string costType, int cost, bool special)
    {
        this.type = type;
        this.title = title;
        this.imagePath = imagePath;
        this.amount = amount;
        this.costType = costType;
        this.cost = cost;
        this.special = special;

        buyCardButton.onClick.AddListener(() => OnButtonClick());

        AssignCardStats();
    }

    //DA AGGIUNGERE IL METODO CHE TI ACQUISTA LE CARTE;
    public void OnButtonClick() {
        Debug.Log("Clicked");
    }

    void AssignCardStats() {
        if (special)
        {
            background.overrideSprite = specialBackground;
            titleBackground.overrideSprite = specialTitleBackground;
            raysImage.overrideSprite = rays;
            greyBackground.color = new Color32(255,224,116,200);
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
        costText.text = HomeUIManager.ConvertCostToString(cost);
        costImage.sprite = Resources.Load<Sprite>("BuyableCard/" + costType);

        SetNativeSizeImage();
    }

    public void SetNativeSizeImage() {
        cardImage.SetNativeSize();
        if (special) {
            titleBackground.SetNativeSize();
            //SPOSTA AL CENTRO DELLA CARTA IL CONTAINER DEL TITOLO
            titleBackground.rectTransform.anchoredPosition = new Vector2(titleBackground.rectTransform.sizeDelta.x / 2, -titleBackground.rectTransform.sizeDelta.y / 2);

        }

    }




}

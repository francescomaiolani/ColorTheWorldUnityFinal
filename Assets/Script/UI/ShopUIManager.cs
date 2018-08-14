using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUIManager : MonoBehaviour {

    public Text goldText, gemsText;
    GameController gameController;
    public delegate void OnShadowToChange();
    public static event OnShadowToChange ChangeShadow;

    public GameObject[] panels;

    public Image[] panelButtonImage;

    private void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        GameController.ChangedStats += UpdateText;
        ChangePanel("Weapon");
        UpdateText();

    }

    void UpdateText()
    {
        goldText.text = gameController.resourceManager.FindResource("gold").GetAmount().ToString();
        gemsText.text = gameController.resourceManager.FindResource("gems").GetAmount().ToString();

        ChangeShadow();
    }

    public void ChangePanel(string section) {
        CloseAllPanel();
        OpenPanel(section);
        ChangeButton(section);
    }

    void CloseAllPanel() {
        foreach (GameObject g in panels) {
            g.SetActive(false);
        }
    }

    void OpenPanel(string section) {
        if (section == "Weapon")
            panels[0].SetActive(true);
        else if (section == "Skin")
            panels[1].SetActive(true);
        else if (section == "Chest")
            panels[2].SetActive(true);
        else if (section == "GoldGem")
            panels[3].SetActive(true);
    }

    void ChangeButton(string section) {
        ChangeAllButtonToNormal();

        if (section == "Weapon")
            panelButtonImage[0].overrideSprite = Resources.Load<Sprite>("ShopButton/WeaponShopIconSelected");
        else if (section == "Skin")
            panelButtonImage[1].overrideSprite = Resources.Load<Sprite>("ShopButton/SkinShopIconSelected");
        else if (section == "Chest")
            panelButtonImage[2].overrideSprite = Resources.Load<Sprite>("ShopButton/ChestShopIconSelected");
        else if (section == "GoldGem")
            panelButtonImage[3].overrideSprite = Resources.Load<Sprite>("ShopButton/BuyGoldGemsShopIconSelected");

        ChangeButtonImageDimensionToNormal();
    }

    void ChangeButtonImageDimensionToNormal() {
        foreach (Image i in panelButtonImage) {
            i.SetNativeSize(); 
        }
    }


    void ChangeAllButtonToNormal() {
        foreach (Image i in panelButtonImage) {
            i.overrideSprite = null;
        }
    }
    private void OnDisable()
    {
        GameController.ChangedStats -= UpdateText;

    }
}

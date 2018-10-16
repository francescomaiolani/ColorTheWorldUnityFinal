using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuUIManager : MonoBehaviour {

    public Text goldText, gemsText, levelText;
    GameController gameController;

    public Image[] leftPanelWeapon;

    public Text baloonName;
    public Button buyBaloon;
    public Text buyBaloonText;
    public Image goldIcon;
    public Image baloonImage;
    public Image LightRay;
    public ShadowLenghtAdapter weaponShadow;

    public GameObject blackPanel;
    public GameObject weaponBoughtPanel;
    public Image weaponBoughtImage;
    public Text weaponBoughtName;


    //public Text weaponPrice;
    public Text baloonLevel;

    public delegate void OnShadowToChange();
    public static event OnShadowToChange ChangeShadow;

    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        GameController.ChangedStats += UpdateText;
        GameController.ChangedStats += UpdateWeaponPanel;
        UpdateText();
        //UpdateWeaponPanel();
        //UpdateLeftPanelWeapon();
    }

    void UpdateText()
    {
        goldText.text = HomeUIManager.ConvertCostToString(gameController.resourceManager.FindResource("gold").GetAmount());
        gemsText.text = HomeUIManager.ConvertCostToString(gameController.resourceManager.FindResource("gems").GetAmount());
        levelText.text = gameController.resourceManager.FindResource("level").GetAmount().ToString();

    }

    void UpdateWeaponPanel() {
        Baloon selectedBaloon = gameController.lastSelectedBaloon;
        baloonLevel.text = selectedBaloon.GetLevel().ToString();
        baloonName.text = selectedBaloon.GetName().ToUpper();
        //weaponPrice.text = selectedWeapon.goldCost.ToString();
        
        //CARICAMENTO DELL'IMMAGINE DELL'ARMA
        Sprite weaponSprite = LoadWeaponSprite(selectedBaloon.GetName(), selectedBaloon.GetAcquired());
        baloonImage.sprite = weaponSprite;
        baloonImage.rectTransform.sizeDelta = new Vector2(baloonImage.sprite.rect.width * 1.15f, baloonImage.sprite.rect.height * 1.15f);
        //weaponShadow.imageReference = weaponImage;
        if (selectedBaloon.GetAcquired())
            LightRay.gameObject.SetActive(true);
        else
            LightRay.gameObject.SetActive(false);

        weaponShadow.Adapt();
        UpdateBuyButton();
    }

    void UpdateLeftPanelWeapon() {
        List<Baloon> allBaloon = gameController.GetAllBaloonList();

        for (int i = 0; i < leftPanelWeapon.Length; i++) {
            leftPanelWeapon[i].sprite = LoadWeaponSprite(allBaloon[i].GetName(), allBaloon[i].GetAcquired());
            //leftPanelWeapon[i].SetNativeSize();
        }
    }

    Sprite LoadWeaponSprite(string name, bool acquired) {
        Sprite returnSprite = null;
        if (!acquired) {
            Sprite[] allSprites = Resources.LoadAll<Sprite>("WeaponSpriteBig/AllWeaponCoverBig");
            foreach (Sprite s in allSprites) {
                if (s.name == name + "Cover")
                    returnSprite = s;               
            }
        }
        else if (acquired)
        {
            Sprite[] allSprites = Resources.LoadAll<Sprite>("WeaponSpriteBig/AllWeaponBig");
            foreach (Sprite s in allSprites)
            {
                if (s.name == name)
                    returnSprite = s;
            }
            LightRay.gameObject.SetActive(true);
        }
        return returnSprite;
    }
    void UpdateBuyButton() {

        if (gameController.lastSelectedBaloon.GetAcquired() == true)  {
            buyBaloonText.text = "EQUIP";
            goldIcon.gameObject.SetActive(false);
            buyBaloon.interactable = true;
        }
        else if (gameController.lastSelectedBaloon.GetLevelToAcquire() > gameController.resourceManager.FindResource("level").GetAmount())  {
            buyBaloonText.text = "HIGHER LEVEL REQUIRED";
            goldIcon.gameObject.SetActive(false);
            buyBaloon.interactable = false;
        }
        else if (!gameController.lastSelectedBaloon.GetAcquired() && gameController.lastSelectedBaloon.GetLevelToAcquire() <= gameController.resourceManager.FindResource("level").GetAmount()) {
            buyBaloonText.text = HomeUIManager.ConvertCostToString(gameController.lastSelectedBaloon.GetGoldCost());
            goldIcon.gameObject.SetActive(true);
            buyBaloon.interactable = true;
        }

    }

    public void SelectBaloon(string nome) {
        Baloon baloonSelected = gameController.FindBaloon(nome);
        gameController.lastSelectedBaloon = baloonSelected;
        if (gameController.lastSelectedBaloon.GetAcquired() == true)
            gameController.actualBaloon = baloonSelected;

        UpdateWeaponPanel();
    }

    public void BuyWeapon() {
        if (gameController.CheckIfEnoughGold(gameController.lastSelectedBaloon.GetGoldCost()) && !gameController.lastSelectedBaloon.GetAcquired())
        {
            gameController.AddGold(-gameController.lastSelectedBaloon.GetGoldCost());
            gameController.FindBaloon(gameController.lastSelectedBaloon.GetName()).SetAcquired(true);
            gameController.actualBaloon = gameController.lastSelectedBaloon;
            gameController.actualBaloon.SetLevel( 1);
            gameController.SaveAllData();
            UpdateWeaponPanel();
            UpdateBuyButton();
            ShowWeaponBoughtPanel(baloonImage.sprite);
            UpdateLeftPanelWeapon();

        }
        else
            Debug.Log("Not enough gold or weapon already owned");
    }

    void ShowWeaponBoughtPanel(Sprite weaponSprite) {
        blackPanel.SetActive(true);
        weaponBoughtPanel.SetActive(true);
        weaponBoughtImage.sprite = weaponSprite;
        weaponBoughtImage.SetNativeSize();
        weaponBoughtName.text = gameController.lastSelectedBaloon.GetName().ToUpper();
        Invoke("CloseWeaponBoughtPanel", 3f);
    }
    void CloseWeaponBoughtPanel() {
        weaponBoughtPanel.SetActive(false);
        blackPanel.SetActive(false);
    }

    private void OnDisable()    {
        GameController.ChangedStats -= UpdateText;
        GameController.ChangedStats -= UpdateWeaponPanel;
    }

}

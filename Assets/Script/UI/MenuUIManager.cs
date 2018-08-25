using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuUIManager : MonoBehaviour {

    public Text goldText, gemsText, levelText;
    GameController gameController;

    public Image[] leftPanelWeapon;

    public Slider damageBar, fireRateBar, heatingBar;
    public Text weaponName;
    public Button buyWeapon;
    public Text buyWeaponText;
    public Image goldIcon;
    public Image weaponImage;
    public Image LightRay;
    public ShadowLenghtAdapter weaponShadow;

    public GameObject blackPanel;
    public GameObject weaponBoughtPanel;
    public Image weaponBoughtImage;
    public Text weaponBoughtName;


    //public Text weaponPrice;
    public Text weaponLevel;

    public delegate void OnShadowToChange();
    public static event OnShadowToChange ChangeShadow;

    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        GameController.ChangedStats += UpdateText;
        GameController.ChangedStats += UpdateWeaponPanel;
        UpdateText();
        UpdateWeaponPanel();
        UpdateLeftPanelWeapon();
    }

    void UpdateText()
    {
        goldText.text = HomeUIManager.ConvertCostToString(gameController.resourceManager.FindResource("gold").GetAmount());
        gemsText.text = HomeUIManager.ConvertCostToString(gameController.resourceManager.FindResource("gems").GetAmount());
        levelText.text = gameController.resourceManager.FindResource("level").GetAmount().ToString();

    }

    void UpdateWeaponPanel() {
        Weapon selectedWeapon = gameController.lastSelectedWeapon;
        weaponLevel.text = selectedWeapon.level.ToString();
        weaponName.text = selectedWeapon.name.ToUpper();
        //weaponPrice.text = selectedWeapon.goldCost.ToString();
        damageBar.value = selectedWeapon.damageIndicator;
        fireRateBar.value = selectedWeapon.fireRateIndicator;
        heatingBar.value = selectedWeapon.heatingRateIndicator;
        //CARICAMENTO DELL'IMMAGINE DELL'ARMA
        Sprite weaponSprite = LoadWeaponSprite(selectedWeapon.name, selectedWeapon.acquired);
        weaponImage.sprite = weaponSprite;
        weaponImage.rectTransform.sizeDelta = new Vector2(weaponImage.sprite.rect.width * 1.15f, weaponImage.sprite.rect.height * 1.15f);
        //weaponShadow.imageReference = weaponImage;
        if (selectedWeapon.acquired)
            LightRay.gameObject.SetActive(true);
        else
            LightRay.gameObject.SetActive(false);

        weaponShadow.Adapt();
        UpdateBuyButton();
    }

    void UpdateLeftPanelWeapon() {
        List<Weapon> allWeapon = gameController.GetAllWeaponList();

        for (int i = 0; i < leftPanelWeapon.Length; i++) {
            leftPanelWeapon[i].sprite = LoadWeaponSprite(allWeapon[i].name, allWeapon[i].acquired);
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

        if (gameController.lastSelectedWeapon.acquired == true)  {
            buyWeaponText.text = "EQUIP";
            goldIcon.gameObject.SetActive(false);
            buyWeapon.interactable = true;
        }
        else if (gameController.lastSelectedWeapon.levelToAcquire > gameController.resourceManager.FindResource("level").GetAmount())  {
            buyWeaponText.text = "HIGHER LEVEL REQUIRED";
            goldIcon.gameObject.SetActive(false);
            buyWeapon.interactable = false;
        }
        else if (!gameController.lastSelectedWeapon.acquired && gameController.lastSelectedWeapon.levelToAcquire <= gameController.resourceManager.FindResource("level").GetAmount()) {
            buyWeaponText.text = HomeUIManager.ConvertCostToString(gameController.lastSelectedWeapon.goldCost);
            goldIcon.gameObject.SetActive(true);
            buyWeapon.interactable = true;
        }

    }

    public void SelectWeapon(string nome) {
        Weapon weaponSelected = gameController.FindWeapon(nome);
        gameController.lastSelectedWeapon = weaponSelected;
        if (gameController.lastSelectedWeapon.acquired == true)
            gameController.actualWeapon = weaponSelected;

        UpdateWeaponPanel();
    }

    public void BuyWeapon() {
        if (gameController.CheckIfEnoughGold(gameController.lastSelectedWeapon.goldCost) && !gameController.lastSelectedWeapon.acquired)
        {
            gameController.AddGold(-gameController.lastSelectedWeapon.goldCost);
            gameController.FindWeapon(gameController.lastSelectedWeapon.name).acquired = true;
            gameController.actualWeapon = gameController.lastSelectedWeapon;
            gameController.actualWeapon.level = 1;
            gameController.SaveAllData();
            UpdateWeaponPanel();
            UpdateBuyButton();
            ShowWeaponBoughtPanel(weaponImage.sprite);
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
        weaponBoughtName.text = gameController.lastSelectedWeapon.name.ToUpper();
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

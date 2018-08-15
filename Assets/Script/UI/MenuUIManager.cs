using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuUIManager : MonoBehaviour {

    public Text goldText, gemsText;
    GameController gameController;

    public Slider damageBar, fireRateBar, heatingBar;
    public Text weaponName;
    public Button buyWeapon;
    public Text buyWeaponText;
    public Image weaponImage;
    public Image LightRay;
    public ShadowLenghtAdapter weaponShadow;

    public GameObject blackPanel;
    public GameObject weaponBoughtPanel;
    public Image weaponBoughtImage;


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
    }

    void UpdateText()
    {
        goldText.text = gameController.resourceManager.FindResource("gold").GetAmount().ToString();
        gemsText.text = gameController.resourceManager.FindResource("gems").GetAmount().ToString();

        ChangeShadow();
    }

    void UpdateWeaponPanel() {
        Weapon selectedWeapon = gameController.lastSelectedWeapon;
        weaponLevel.text = selectedWeapon.level.ToString();
        weaponName.text = selectedWeapon.name;
        //weaponPrice.text = selectedWeapon.goldCost.ToString();
        damageBar.value = selectedWeapon.damageIndicator;
        fireRateBar.value = selectedWeapon.fireRateIndicator;
        heatingBar.value = selectedWeapon.heatingRateIndicator;
        LoadWeaponSprite(selectedWeapon.name, selectedWeapon.acquired);
        UpdateBuyButton();
        ChangeShadow();
    }

    void LoadWeaponSprite(string name, bool acquired) {
        if (!acquired) {
            Sprite[] allSprites = Resources.LoadAll<Sprite>("WeaponSpriteBig/AllWeaponCoverBig");
            foreach (Sprite s in allSprites) {
                if (s.name == name + "Cover")
                    weaponImage.sprite = s;
            }

            LightRay.gameObject.SetActive(false);
        }
        else if (acquired)
        {
            Sprite[] allSprites = Resources.LoadAll<Sprite>("WeaponSpriteBig/AllWeaponBig");
            foreach (Sprite s in allSprites)
            {
                if (s.name == name)
                    weaponImage.sprite = s;
            }
            LightRay.gameObject.SetActive(true);

        }

        weaponImage.rectTransform.sizeDelta = new Vector2(weaponImage.sprite.rect.width, weaponImage.sprite.rect.height);
        weaponShadow.imageReference = weaponImage;
        weaponShadow.Adapt();
    }
    void UpdateBuyButton() {

        if (gameController.lastSelectedWeapon.acquired == true)  {
            buyWeaponText.text = "EQUIP";
            buyWeapon.interactable = true;
        }
        else if (gameController.lastSelectedWeapon.levelToAcquire > gameController.resourceManager.FindResource("level").GetAmount())  {
            buyWeaponText.text = "HIGHER LEVEL REQUIRED";
            buyWeapon.interactable = false;

        }
        else if (!gameController.lastSelectedWeapon.acquired && gameController.lastSelectedWeapon.levelToAcquire <= gameController.resourceManager.FindResource("level").GetAmount()) {
            buyWeaponText.text = "BUY " + gameController.lastSelectedWeapon.goldCost.ToString();
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
        }
        else
            Debug.Log("Not enough gold or weapon already owned");
    }

    void ShowWeaponBoughtPanel(Sprite weaponSprite) {
        blackPanel.SetActive(true);
        weaponBoughtPanel.SetActive(true);
        weaponBoughtImage.sprite = weaponSprite;
        weaponBoughtImage.SetNativeSize();
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

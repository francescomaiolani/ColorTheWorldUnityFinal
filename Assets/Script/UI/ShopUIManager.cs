using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUIManager : MonoBehaviour {

    public Text goldText, gemsText, levelText;
    GameController gameController;
    public delegate void OnShadowToChange();
    public static event OnShadowToChange ChangeShadow;

    BuyableCard[] buyableCards;
    public GameObject buyableCardPrefab;
    public GameObject cardPanel;



    private void Start()
    {
        buyableCards = new BuyableCard[3];
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        GameController.ChangedStats += UpdateText;
        UpdateText();

        CreateBuyableCards();
    }

    void UpdateText()
    {
        goldText.text = HomeUIManager.ConvertCostToString(gameController.resourceManager.FindResource("gold").GetAmount());
        gemsText.text = HomeUIManager.ConvertCostToString(gameController.resourceManager.FindResource("gems").GetAmount());
        levelText.text = gameController.resourceManager.FindResource("level").GetAmount().ToString();
    }


//////////////////////////////// METODI PER LE CARTE ACQUISTABILI//////////////////////////////////////////

    void CreateBuyableCards() {

        for (int i = 0; i < 3; i++) {

            //int probability = Random.Range(0, 100);
            int probability = 10;

            int probabilitySpecialCard = Random.Range(0, 100);
            bool specialCard = false;

            //SE SONO ALL'ULTIMA ITERARZIONE HO UNA CHANCE DI BECCARE UNA CARTA SPECIAL
            if (probabilitySpecialCard <= 50 && i == 2)
                specialCard = true;

            // A SECONDA DI COSA E' CAPITATO CREA UNA CARTA PIUTTOSTO CHE UN'ALTRA
            if (probability >= 0 && probability < 33) 
                CreateWeaponCard(i, specialCard);
            
            else if (probability >= 33 && probability < 66)
                CreateWallCard(i, specialCard);
            
            else if (probability >= 66 && probability <= 100)
                CreateSpecialCard(i, specialCard);           
        }
    }

    void CreateWeaponCard(int index, bool specialCard) {

        BuyableCard cardCreated = CreateCard(index);

        List<Weapon> acquiredWeapon = gameController.GetAcquiredWeapon();
        int weaponIndex = Random.Range(0, acquiredWeapon.Count);
        Weapon chosenWeapon = acquiredWeapon[weaponIndex];
        if (specialCard)
            cardCreated.SetCardStats("Weapon", chosenWeapon.name, "BuyableCard/Weapon/" + chosenWeapon.name , Random.Range(40,50),  "gold", 5000, specialCard );
        else
            cardCreated.SetCardStats("Weapon", chosenWeapon.name, "BuyableCard/Weapon/" + chosenWeapon.name, Random.Range(8, 20), "gold", 1000, specialCard);

    }

    void CreateWallCard(int index, bool specialCard)
    {

    }

    void CreateSpecialCard(int index, bool specialCard)
    {

    }

    BuyableCard CreateCard(int index) {

        //CREA LA CARTA E LA AGGIUNGE ALL'ARRAY 
        GameObject card = Instantiate(buyableCardPrefab, new Vector2(0, 0), Quaternion.identity);
        card.transform.SetParent(cardPanel.transform);
        BuyableCard cardComponent = card.GetComponent<BuyableCard>();
        AddCardToCardArray(cardComponent, index);
        //AGGIUSTA LA POSIZIONE E LO SCALE NEL CANVAS CHE SONO SBALLATE SE NO
        RectTransform transformComponent = card.GetComponent<RectTransform>();
        transformComponent.localScale = new Vector3(1, 1, 1);
        transformComponent.anchoredPosition = new Vector2(-440 + index * 440, -50);

        return cardComponent;
    }

    void AddCardToCardArray(BuyableCard component, int index) {
        buyableCards[index] = component;
    }

//////////////////////////////// FINE METODI PER LE CARTE ACQUISTABILI//////////////////////////////////////////


    private void OnDisable()
    {
        GameController.ChangedStats -= UpdateText;

    }
}

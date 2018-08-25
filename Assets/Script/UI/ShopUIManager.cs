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

    public List<Card> chestCard;
    public GameObject card;
    public GameObject goldenCard;
    public GameObject chestCardPanel;


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
    public void BuyChest(string chestType) {

        int cost = SelectChestCost(chestType);
        string costType = SelectChestCostType(chestType);

        if (costType == "gold" && gameController.CheckIfEnoughGold(cost))
        {
            gameController.AddGold(-cost);
            CreateChestCard(Random.Range(3, 5));
        }
        else if (costType == "gems" && gameController.CheckIfEnoughGems(cost)) {
            gameController.AddGems(-cost);
            CreateChestCard(Random.Range(3, 5));
        }

    }

    string SelectChestCostType(string type) {

        if (type == "platinum")
            return "gems";
        else
            return "gold";
    }
    int SelectChestCost(string chestType) {
        switch (chestType) {
            case "iron":
                return 500;
            case "bronze":
                return 1000;
            case "silver":
                return 2000;
            case "gold":
                return 5000;
            case "platinum":
                return 50;
        }
        return 0;
    }

    public void CreateChestCard(int cardAmount )
    {
        chestCard = new List<Card>();
        int special = 0;
        chestCardPanel.SetActive(true);

        for (int i = 0; i < cardAmount; i++)
        {
            if (i == cardAmount - 1)
                special = Random.Range(0, 100);

            if (special < 50)
            {
                GameObject cardInstance = Instantiate(card, new Vector3(0, 0, 0), Quaternion.identity, chestCardPanel.transform);
                cardInstance.transform.localScale = new Vector3(1, 1, 1);
                cardInstance.transform.localPosition = new Vector3(-400 *(i - cardAmount/2),0, 0);
                Card cardComponent = cardInstance.GetComponent<Card>();
                chestCard.Add(cardComponent);
            }
            else {
                GameObject cardInstance = Instantiate(goldenCard, new Vector3(0, 0, 0), Quaternion.identity, chestCardPanel.transform);
                cardInstance.transform.localScale = new Vector3(1, 1, 1);
                cardInstance.transform.localPosition = new Vector3(-400*(i - cardAmount / 2), 0, 0);
                Card cardComponent = cardInstance.GetComponent<Card>();
                chestCard.Add(cardComponent);

            }
        }
    }

    public void CollectChestCard() {
        foreach (Card c in chestCard) {
            if (c.GetCardType() == "weapon")
                gameController.AddWeaponCard(c.GetTitle(), c.GetAmount());
            Debug.Log("Added" + c.GetAmount());
        }

        chestCardPanel.SetActive(false);
        gameController.SaveAllData();
    }


//////////////////////////////// FINE METODI PER LE CARTE ACQUISTABILI//////////////////////////////////////////


    private void OnDisable()
    {
        GameController.ChangedStats -= UpdateText;

    }
}

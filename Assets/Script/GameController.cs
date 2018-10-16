using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    //int gold, gems, level, experience;
    GameController instance;
    public Color32 lastSceneTransitionColorUsed;

    List<Baloon> allBaloon = new List<Baloon>();
    public ResourceManager resourceManager;
    public Baloon actualBaloon;
    public Baloon lastSelectedBaloon;

    public int actualWave;

    public delegate void OnStatsChanged();
    public static event OnStatsChanged ChangedStats;

    //SINGLETON
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }

        //TUTTE LE RISORSE VENGONO CARICATE DIRETTAMENTE DAL RESOURCE MANAGER
        resourceManager = new ResourceManager();
        DontDestroyOnLoad(this.gameObject);
    }

    void Start () {

        PlayerPrefs.DeleteAll();
        Application.targetFrameRate = 60;
        EnemyVariable.EnemyDeadGold += AddGold;
        GameChest.GiveGold += AddGold;
        PlayerPrefs.SetInt("actualWave",1);


        PopulateAllWeapon();
        LoadSavedVariable();
    }

    
    void LoadSavedVariable() {

        //INIZIALIZZAZIONE ARMI
        foreach (Baloon b in allBaloon) {
            if (PlayerPrefs.GetString(b.GetName() + "acquired") == "true")
                b.SetAcquired(true);
            else
                b.SetAcquired(false);

            if (PlayerPrefs.HasKey(b.GetName() + "level"))
                b.SetLevel(PlayerPrefs.GetInt(b.GetName() + "level"));
            else
                b.SetLevel(0);

            if (PlayerPrefs.HasKey(b.GetName() + "cardNumber"))
                b.SetCardNumber(PlayerPrefs.GetInt(b.GetName() + "cardNumber"));
            else
                b.SetCardNumber(0);
        }

        //ASSEGNA L'ARMA INIZIALE SULLA BASE DI COSA HAI USATO PER ULTIMO
        if (FindBaloon(PlayerPrefs.GetString("lastUsedBaloon")) == null)
            actualBaloon = FindBaloon("normal");
        else
            actualBaloon = FindBaloon(PlayerPrefs.GetString("lastUsedBaloon"));

        lastSelectedBaloon = actualBaloon;

        //CARICA L'ULTIMA ONDATA DI NEMICI A CUI SEI ARRIVATO
        actualWave = PlayerPrefs.GetInt("actualWave");
        ChangedStats();
    }

    public void ResetWeaponData() {

        resourceManager.SetResourceAmount("gold", 100000);
        foreach (Baloon b in allBaloon) {
            b.SetAcquired(false);
            b.SetLevel(0);
        }
        allBaloon[0].SetAcquired( true);
        allBaloon[0].SetLevel(1);

        SaveAllData();
        LoadSavedVariable();
    }

    public void SaveAllData() {

        PlayerPrefs.SetInt("gold", resourceManager.FindResource("gold").GetAmount());
        PlayerPrefs.SetInt("gems", resourceManager.FindResource("gems").GetAmount());
        PlayerPrefs.SetInt("level", resourceManager.FindResource("level").GetAmount());
        PlayerPrefs.SetInt("experience", resourceManager.FindResource("experience").GetAmount());
        PlayerPrefs.SetInt("actualWave", actualWave);

        //SALVATAGGIO DEI DATI UTILI DELLE ARMI 
        foreach (Baloon b in allBaloon) {
            if (b.GetAcquired() == true)
                PlayerPrefs.SetString(b.GetName() + "acquired", "true");
            else
                PlayerPrefs.SetString(b.GetName() + "acquired", "false");

            PlayerPrefs.SetInt(b.GetName() + "level", b.GetLevel());
            PlayerPrefs.SetInt(b.GetName() + "cardNumber", b.GetCardNumber());

        }
    }

    void PopulateAllWeapon() {

        allBaloon.Add(new Baloon("normal", 3, "long", 10, 15 , 1 ));
        allBaloon.Add(new Baloon("triple", 1, "long", 8, 10, 3));
        allBaloon.Add(new Baloon("big", 8, "medium", 20, 30, 1));
        allBaloon.Add(new Baloon("gas", 4, "medium", 30, 10, 1, 2, new Debuf("gas", 4f), 4f));
    }

    //METODO CHE RESTITUISCE L'OGGETTO ARMA CON NOME INDICATO
    public Baloon FindBaloon(string nome) {
        foreach (Baloon b in allBaloon) {
            if (b.GetName() == nome)
                return b;
        }
        return null;
    }

    //METODO CHE RITORNA LA LISTA DI TUTTE LE ARMI POSSEDUTE 
    public List<Baloon> GetAcquiredBaloon() {
        List<Baloon> list = new List<Baloon>();
        foreach (Baloon b in allBaloon) {
            if (b.GetAcquired() == true)
                list.Add(b );
        }
        return list;
    }

    //METODO CHE AGGIUNGE A UN ARMA DELLA LISTA DI WEAPON UNA QUANTITA' DI CARTE
    public void AddBaloonCard(string nome, int amount) {
        Baloon chosenBaloon = FindBaloon(nome);
        chosenBaloon.AddToCardNumber(amount);
        Debug.Log(chosenBaloon.GetCardNumber());
    }

    //METODO CHE CONTROLLA SE SI HA ABBASTANZA ORO PER COMPRARE QUALCOSA
    public bool CheckIfEnoughGold(int amount) {
        if (amount <= resourceManager.FindResource("gold").GetAmount())
            return true;
        else
            return false;
    }

    public bool CheckIfEnoughGems(int amount)
    {
        if (amount <= resourceManager.FindResource("gems").GetAmount())
            return true;
        else
            return false;
    }

    public void AddGold(int amount) {
        resourceManager.FindResource("gold").AddToAmount(amount);
        ChangedStats();
    }

    public void AddGems(int amount) {
        resourceManager.FindResource("gems").AddToAmount(amount);
        ChangedStats();
    }
    public void AddExperience(int amount)
    {
        resourceManager.FindResource("experience").AddToAmount(amount);
        ChangedStats();
    }
    public void AddLevel(int amount)
    {
        resourceManager.FindResource("level").AddToAmount(amount);
        ChangedStats();
    }


    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //GETTERS

    public List<Baloon> GetAllBaloonList() {
        return allBaloon;
    }
   
}

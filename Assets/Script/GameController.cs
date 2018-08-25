using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    //int gold, gems, level, experience;
    GameController instance;
    public Color32 lastSceneTransitionColorUsed;

    List<Weapon> allWeapon = new List<Weapon>();
    public ResourceManager resourceManager;
    public Weapon actualWeapon;
    public Weapon lastSelectedWeapon;

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

        Application.targetFrameRate = 60;
        EnemyVariable.EnemyDeadGold += AddGold;
        GameChest.GiveGold += AddGold;
        PlayerPrefs.SetString("LittleGunacquired", "true");
        PlayerPrefs.SetInt("LittleGunlevel", 1);
        PlayerPrefs.SetInt("actualWave",1);


        PopulateAllWeapon();
        LoadSavedVariable();
    }

    
    void LoadSavedVariable() {

        //INIZIALIZZAZIONE ARMI
        foreach (Weapon w in allWeapon) {
            if (PlayerPrefs.GetString(w.name + "acquired") == "true")
                w.SetAcquired(true);
            else
                w.SetAcquired(false);

            if (PlayerPrefs.HasKey(w.name + "level"))
                w.level = PlayerPrefs.GetInt(w.name + "level");
            else
                w.level = 0;

            if (PlayerPrefs.HasKey(w.name + "cardNumber"))
                w.cardNumber = PlayerPrefs.GetInt(w.name + "cardNumber");
            else
                w.cardNumber = 0;
        }

        //ASSEGNA L'ARMA INIZIALE SULLA BASE DI COSA HAI USATO PER ULTIMO
        if (FindWeapon(PlayerPrefs.GetString("lastUsedWeapon")) == null)
            actualWeapon = FindWeapon("LittleGun");
        else
            actualWeapon = FindWeapon(PlayerPrefs.GetString("lastUsedWeapon"));

        lastSelectedWeapon = actualWeapon;

        //CARICA L'ULTIMA ONDATA DI NEMICI A CUI SEI ARRIVATO
        actualWave = PlayerPrefs.GetInt("actualWave");
        ChangedStats();
    }

    public void ResetWeaponData() {

        resourceManager.SetResourceAmount("gold", 100000);
        foreach (Weapon w in allWeapon) {
            w.acquired = false;
            w.level = 0;
        }
        allWeapon[0].acquired = true;
        allWeapon[0].level = 1;

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
        foreach (Weapon w in allWeapon) {
            if (w.acquired == true)
                PlayerPrefs.SetString(w.name + "acquired", "true");
            else
                PlayerPrefs.SetString(w.name + "acquired", "false");

            PlayerPrefs.SetInt(w.name + "level", w.level);
            PlayerPrefs.SetInt(w.name + "cardNumber", w.cardNumber);

        }
    }

    void PopulateAllWeapon() {

        allWeapon.Add(new Weapon("LittleGun", "", 10, 0.5f, 2.2f, 1.8f, 1, 1, 0));
        allWeapon.Add(new Weapon("Revolver", "", 14, 0.55f, 2.2f, 1.8f, 1, 1, 0));
        allWeapon.Add(new Weapon("Uzi", "", 12, 0.24f, 2f, 1.25f, 1, 1, 0));
        allWeapon.Add(new Weapon("Assault", "", 20, 0.3f, 2f, 1.25f, 1, 1, 0));
        allWeapon.Add(new Weapon("Shotgun", "", 20, 1f, 2f, 1.25f, 5, 1, 4));
        allWeapon.Add(new Weapon("Sniper", "", 100, 0.8f, 2f, 1.25f, 1, 3, 0));

        allWeapon[0].SetShopWeaponStats(0, 0, 8, 50, 40, 40);
        allWeapon[1].SetShopWeaponStats(0, 1000, 12, 45, 40, 40);
        allWeapon[2].SetShopWeaponStats(0, 2000, 10, 80, 40, 40);
        allWeapon[3].SetShopWeaponStats(0, 5000, 30, 65, 40, 40);
        allWeapon[4].SetShopWeaponStats(0, 10000, 60, 20, 40, 40);
        allWeapon[5].SetShopWeaponStats(0, 10000, 86, 30, 40, 40);

    }

    //METODO CHE RESTITUISCE L'OGGETTO ARMA CON NOME INDICATO
    public Weapon FindWeapon(string nome) {
        foreach (Weapon w in allWeapon) {
            if (w.name == nome)
                return w;
        }
        return null;
    }

    //METODO CHE RITORNA LA LISTA DI TUTTE LE ARMI POSSEDUTE 
    public List<Weapon> GetAcquiredWeapon() {
        List<Weapon> list = new List<Weapon>();
        foreach (Weapon w in allWeapon) {
            if (w.acquired == true)
                list.Add(w);
        }
        return list;
    }

    //METODO CHE AGGIUNGE A UN ARMA DELLA LISTA DI WEAPON UNA QUANTITA' DI CARTE
    public void AddWeaponCard(string nome, int amount) {
        Weapon chosenWeapon = FindWeapon(nome);
        chosenWeapon.cardNumber += amount;
        Debug.Log(chosenWeapon.cardNumber);
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

    public List<Weapon> GetAllWeaponList() {
        return allWeapon;
    }
   
}

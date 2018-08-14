using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    //int gold, gems, level, experience;
    GameController instance;

    List<Weapon> allWeapon = new List<Weapon>();
    public ResourceManager resourceManager;
    public Weapon actualWeapon;
    public Weapon lastSelectedWeapon;

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
        resourceManager = new ResourceManager();
        DontDestroyOnLoad(this.gameObject);
    }

    void Start () {       
        EnemyVariable.EnemyDeadGold += AddGold;
        PlayerPrefs.SetString("LittleGunacquired", "true");
        PlayerPrefs.SetInt("LittleGunlevel", 1);


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
        actualWeapon = FindWeapon(PlayerPrefs.GetString("lastUsedWeapon"));
        if (actualWeapon == null)  
            actualWeapon = FindWeapon("LittleGun");

        lastSelectedWeapon = actualWeapon;
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
        ChangedStats();
    }

    public void SaveAllData() {
        PlayerPrefs.SetInt("gold", resourceManager.FindResource("gold").GetAmount());
        PlayerPrefs.SetInt("gems", resourceManager.FindResource("gems").GetAmount());
        PlayerPrefs.SetInt("level", resourceManager.FindResource("level").GetAmount());
        PlayerPrefs.SetInt("experience", resourceManager.FindResource("experience").GetAmount());

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
        allWeapon.Add(new Weapon("Sniper", "", 100, 0.8f, 2f, 1.25f, 5, 1, 4));

        allWeapon[0].SetShopWeaponStats(0, 0, 8, 50, 40, 40);
        allWeapon[1].SetShopWeaponStats(0, 1000, 12, 45, 40, 40);
        allWeapon[2].SetShopWeaponStats(0, 2000, 10, 80, 40, 40);
        allWeapon[3].SetShopWeaponStats(0, 5000, 30, 65, 40, 40);
        allWeapon[4].SetShopWeaponStats(0, 10000, 60, 20, 40, 40);
        allWeapon[5].SetShopWeaponStats(0, 10000, 86, 30, 40, 40);

    }


    public Weapon FindWeapon(string nome) {
        foreach (Weapon w in allWeapon) {
            if (w.name == nome)
                return w;
        }

        return null;
    }
    public bool CheckIfEnoughGold(int amount) {
        if (amount <= resourceManager.FindResource("gold").GetAmount())
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
   
}

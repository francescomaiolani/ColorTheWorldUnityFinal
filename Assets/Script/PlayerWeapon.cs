using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeapon : MonoBehaviour {

    public Timer fireRateTimer;
    public Weapon actualWeapon;
    public TimerManager timerManager;
    public GameObject bullet;
    public Slider heatingBar;
    public Transform shootingPoint;

    //public Text heatingText;
    public GameController gameController;


    private bool possoSparare;
    private bool shootButtonDown;

    private float actualHeating;
    private float minHeating = 10;

	void Start () {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        actualWeapon = gameController.actualWeapon;
        actualHeating = 100;
        AssignTimer();
        InvokeRepeating("UpdateHeating", 0, 0.05f);
	}

    void UpdateHeating() {
        if (!shootButtonDown)
            actualHeating += actualWeapon.reloadTick;
        else
            actualHeating -= actualWeapon.heatingTick;

        actualHeating = Mathf.Clamp(actualHeating, 0f, 100f);
        //heatingText.text = actualHeating.ToString();
        heatingBar.value = actualHeating;       
    }

    void AssignTimer() {
        fireRateTimer = new Timer(actualWeapon.fireRate, false);
        timerManager.AddTimer(fireRateTimer);
        fireRateTimer.StartTimer();
        fireRateTimer.TimerEnded += FireRateOver;
    }

    void Shoot() {
        if (CheckIfEnoughHeating()) {

            for (int i = 0; i < actualWeapon.bulletCount; i++)
            {
                GameObject bulletInstance = Instantiate(bullet, shootingPoint.position, Quaternion.identity);
                BulletMovement bulletMovemennt = bulletInstance.GetComponent<BulletMovement>();
                bulletMovemennt.SetBulletStats(actualWeapon.damagexBullet, actualWeapon.perforante, actualWeapon.bulletCount, actualWeapon.bulletSpread,i);
            }

            fireRateTimer.StartTimer();
            possoSparare = false;
        }
    }

    bool CheckIfEnoughHeating() {
        if (actualHeating > minHeating)
            return true;
        else
            return false;
    }

    private void Update() {
        if (shootButtonDown && possoSparare)
            Shoot();
    }

    void FireRateOver() {
        possoSparare = true;
        if (shootButtonDown) {
            Shoot();
        }
    }

    public void ShootButtonDown() {
        shootButtonDown = true;
        if (fireRateTimer.GetTimer() >= fireRateTimer.GetMaxTimer())
            Shoot();
    }

    public void ShootButtonUp() {
        shootButtonDown = false;
       
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

    float actualAngle;
    float actualDistance;
    public Transform baloonPosition;

    GameObject actualBaloonInstance;
    public Transform baloonContainer;

    public Color32[] stainColors;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.timeScale == 1)
            Time.timeScale = 0;
        
    }
    private void Start()
    {
        Application.targetFrameRate = 60;
        ChargeBaloon();
    }
    public GameObject baloonPrefab;
    public Baloon actualBaloon = new Baloon("normal", 3, "long", 10, 10, 1);

    //shootDistance e' un coefficiente da 0 a 1 che moltiplica la velocita' massima
    public void ShootBaloon(float angle, float shootDistance ) {
        actualAngle = angle;
        actualDistance = shootDistance;         
    }

    public void Shoot() {
        actualBaloonInstance.transform.SetParent(baloonContainer);
        BaloonMovement baloonMovement = actualBaloonInstance.GetComponent<BaloonMovement>();
        baloonMovement.ShootBaloon(actualAngle, actualDistance, actualBaloon, stainColors[Random.Range(0, stainColors.Length)]);
        actualBaloonInstance = null;
    }

    public void ChargeBaloon() {
        if (actualBaloonInstance == null)
           actualBaloonInstance = Instantiate(baloonPrefab, baloonPosition.position, Quaternion.identity,baloonPosition);
    }
}

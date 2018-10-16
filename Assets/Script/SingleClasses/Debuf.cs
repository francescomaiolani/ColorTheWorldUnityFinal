using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debuf {

    //slow, venom, fire, ecc...
    string type;
    float time;
    float damageXTime;
    float damageTimer;

    public Debuf(string type, float time) {
        this.type = type;
        this.time = time;
    }

    public Debuf(string type, float time, float damageXTime, float damageTimer)
    {
        this.type = type;
        this.time = time;
        this.damageXTime = damageXTime;
        this.damageTimer = damageTimer;
    }

}

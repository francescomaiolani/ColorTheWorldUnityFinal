using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveData : ScriptableObject {

    public int waveIndex;

    public EnemyWaveData[] waves;   
}

[System.Serializable]
public struct EnemyWaveData
{
    public string enemyName;
    public bool burst;
    public int maxNumber;
    public float initialSpawnTime;
    public float interSpawnTime;
}

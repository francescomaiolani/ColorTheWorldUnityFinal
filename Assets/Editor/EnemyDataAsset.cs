using UnityEngine;
using UnityEditor;

public class EnemyDataAsset
{
    [MenuItem("Assets/Create/EnemyData")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<EnemyData>();
    }
}

public class WaveDataAsset
{
    [MenuItem("Assets/Create/WaveData")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<WaveData>();
    }
}
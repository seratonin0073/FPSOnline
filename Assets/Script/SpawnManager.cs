using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [HideInInspector] public static SpawnManager Instance;
    private SpawnPoint[] spawns;
    
    void Awake()
    {
        Instance = this;
        spawns = GetComponentsInChildren<SpawnPoint>();
    }

    public Transform GetSpawnPoint()
    {
        return spawns[Random.Range(0, spawns.Length)].transform;
    }
}

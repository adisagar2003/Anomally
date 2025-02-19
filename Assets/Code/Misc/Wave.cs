using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    [SerializeField] private GameObject[] enemies;

    public static int id { get; private set; } = 0;
    public int waveId { get; private set; }

    Wave()
    {
        waveId = ++id;

    }
    public GameObject[] GetEnemies()
    {
        return enemies;
    }
}

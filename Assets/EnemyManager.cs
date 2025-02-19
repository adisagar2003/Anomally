using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    // Need to implement singleton pattern 
    public static EnemyManager _instance { get; private set; }
    int waveIndex = 0;
    int enemyCount = 0;
    public Wave[] waves;
    [SerializeField] private Transform[] spawnPoints;
    private string debugString = "";

    private void OnEnable()
    {
        BaseEnemy.DeathEvent += ReduceEnemyCount;
    }

    private void OnDisable()
    {
        BaseEnemy.DeathEvent -= ReduceEnemyCount;
    }
    void Awake()
    {
        // prevents copies
        if (_instance == null)
        {
            _instance = new EnemyManager();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [ContextMenu("Initialize")]
    public void Initialize()
    {
        GameObject[] enemiesRef = waves[waveIndex].GetEnemies();

        // spawn each one at random transform points
        foreach ( GameObject enemy in enemiesRef)
        {
            Transform randomTransform = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(enemy, randomTransform);
            enemyCount += 1;
        }

        waveIndex += 1;
    }
    // Update is called once per frame
    void Update()
    {
        debugString += $"Enemy Count: {enemyCount}" +
            $"Wave Index: {waveIndex}";
    }

    private void ReduceEnemyCount()
    {
        enemyCount -= 1;
    }

}

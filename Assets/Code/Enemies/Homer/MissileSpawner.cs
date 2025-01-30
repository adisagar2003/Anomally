using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSpawner : MonoBehaviour
{
    [SerializeField] private float spawnTime;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private GameObject missile;
    bool isSpawning = false;
    private void FixedUpdate()
    {
        
    }

    public void StartAttack()
    {
        if (!isSpawning)
        {
            StartCoroutine(SpawnMissile());
        }
    }

    private IEnumerator SpawnMissile()
    {
        isSpawning = true;
        GameObject missileSpawned = Instantiate(missile);
        missileSpawned.transform.position = spawnPoint.transform.position;
        yield return new WaitForSeconds(spawnTime);
        isSpawning = false;
    }
}

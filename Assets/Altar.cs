using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : MonoBehaviour
{
    bool isInRangeOfActivation = false;
    private EnemyManager enemyManager;

    private void Awake()
    {
        enemyManager = GameObject.FindAnyObjectByType<EnemyManager>();
    }
    public void SetIsInRangeOfActivation(bool value)
    {
        isInRangeOfActivation = value;
    }

    public void Initialize()
    {
        // Start the altar 
        enemyManager.Initialize();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager
{
    // Start is called before the first frame update
    public List<GameObject> EnemiesWaveOne;
    public List<GameObject> EnemiesWaveTwo;
    public List<GameObject> EnemiesWaveThree;
    public List<GameObject> EnemiesWaveFour;
    public List<GameObject> EnemiesWaveFive;
    public List<GameObject> EnemiesWaveSix;
    

    public List<GameObject>[] waves;
   

  
    void Start()
    {
        waves = new List<GameObject>[] { EnemiesWaveOne, EnemiesWaveTwo, EnemiesWaveThree, EnemiesWaveFour, EnemiesWaveFive, EnemiesWaveSix };
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Initialize()
    {
        
    }
}

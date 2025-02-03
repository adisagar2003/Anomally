using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dasher : BaseEnemy
{

    public enum DasherState
    {
        Patrol, 
        Alert,
        Attack
    }

    private DasherState currentState;

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        StopAllCoroutines();   
    }

    
    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    public override void Damage(float damageAmount)
    {
        throw new System.NotImplementedException();
    }

    public override void DisableAllAttacks()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            default:
            case DasherState.Patrol:
                // get random x position from 10-20 distance,
                Debug.Log("Dasher should Patrol");
                break;
            case DasherState.Attack:
                Debug.Log("Dash!");
                break;
            
        }
    }
}

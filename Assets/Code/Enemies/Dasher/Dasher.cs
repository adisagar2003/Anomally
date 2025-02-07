using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dasher : BaseEnemy
{
    [SerializeField] private GameObject eye;
    [SerializeField] private float attackDistance = 1.35f;
    private DasherMovement dasherMovement;
    public enum DasherState
    {
        Patrol, 
        Alert,
        Attack,
        Attacking,
        Hurt
    }

    private DasherState currentState;

    void Start()
    {
        currentState = DasherState.Patrol;
        dasherMovement = GetComponent<DasherMovement>();
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(eye.transform.position, Vector2.left * attackDistance);
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        StopAllCoroutines();   
    }

    
    public override void Attack()
    {
        
    }

    public override void Damage(float damageAmount)
    {

    }

    public override void DisableAllAttacks()
    {

    }

    public override void TakeDamage(float amount)
    {
        health -= amount;
        // recoil backwards 
        dasherMovement.RecoilBack();
    }

    void FixedUpdate()
    {
        switch (currentState)
        {
            default:
            case DasherState.Patrol:
                // get random x position from 10-20 distance,
                Debug.Log("Dasher should Patrol");
                DetectPlayer();
                break;
            case DasherState.Attack:
                StartCoroutine(AttackCoroutine());
                
                break;
            case DasherState.Alert:
                dasherMovement.ChasePlayer();
                DetectPlayer();
                break;
        }

        // raycasting to check if player is visible 
        

    }

    private void DetectPlayer()
    {
        RaycastHit2D playerHit = Physics2D.Raycast(eye.transform.position, Vector2.right * attackDistance);

        if (playerHit.transform.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player hit");
            currentState = DasherState.Attack;
        }
    }

    // Attack combat
    private IEnumerator AttackCoroutine()
    {
        currentState = DasherState.Attacking;
        // future... dash functionality.
        yield return new WaitForSeconds(1.3f);
        currentState = DasherState.Alert;
    }


}

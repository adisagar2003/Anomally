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

    [SerializeField] private DasherState currentState;

    void Start()
    {
        currentState = DasherState.Patrol;
        dasherMovement = GetComponent<DasherMovement>();
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(eye.transform.position, new Vector2(eye.transform.rotation.y,0) * attackDistance);
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
      

        if (health < 0)
        {
            Death();
        }
    }

    public override void Death()
    {
        base.Death();
    }

    void FixedUpdate()
    {
        
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartAttack(collision.transform.position);   
        }
    }

    private void StartAttack(Vector3 playerCurrentPosition)
    {
        //if (currentState != DasherState.Hurt && currentState != DasherState.Attack)
        //{
            currentState = DasherState.Attack;
            // dash towards the last player's x location listed
            dasherMovement.DashTowardsPlayer(playerCurrentPosition);
        //}
    }


}

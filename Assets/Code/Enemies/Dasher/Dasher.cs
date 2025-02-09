using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dasher : BaseEnemy
{
    [SerializeField] private GameObject eye;
    [SerializeField] private float attackDistance = 1.35f;
    private Player player;
    private DasherMovement dasherMovement;
    public enum DasherState
    {
        Idle, 
        Alert,
        Attack,
        Attacking,
        Hurt
    }

    [SerializeField] private DasherState currentState;

    void Start()
    {
        player = FindFirstObjectByType<Player>();
        currentState = DasherState.Idle;
        dasherMovement = GetComponent<DasherMovement>();
        StartCoroutine(Spawn());
    }


    public IEnumerator Spawn()
    {
        yield return new WaitForSeconds(1.4f);
        currentState = DasherState.Alert;
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
        health -= 5f;
        
        if (health < 0)
        {
            Death();
        } else
        {
            Debug.Log("Knockback applied");
            dasherMovement.Knockback((player.transform.position-transform.position).normalized);
        }

        
    }

    public override void Death()
    {
        base.Death();
    }

    void FixedUpdate()
    {
        StartChase();
    }

    public void StartChase()
    {
        if (currentState == DasherState.Alert)
        {
            dasherMovement.ChasePlayer();
        }
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



    public void GetHurt()
    {
        Debug.Log("Player has hurt dasher");
    }
    public void StartAttack(Vector3 playerCurrentPosition)
    {
        //if (currentState != DasherState.Hurt && currentState != DasherState.Attack)
        //{
            currentState = DasherState.Attack;
            // dash towards the last player's x location listed
            dasherMovement.DashTowardsPlayer(playerCurrentPosition);
        //}
    }


}

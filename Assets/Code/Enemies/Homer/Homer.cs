using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homer : BaseEnemy
{

    public enum HomerState
    {
        Idle,
        Attack,
        Hurt,
        Death
    }

    [SerializeField] private float deathTime = 1.2f;
    private HomerState currentState;
    private SpriteRenderer sprite;
    private MissileSpawner missileSpawner;
    void Start()
    {
        currentState = HomerState.Idle;
        sprite = GetComponentInChildren<SpriteRenderer>();
        missileSpawner = GetComponent<MissileSpawner>();
    }


    private void FixedUpdate()
    {
        if (currentState == HomerState.Attack)
        {
            missileSpawner.StartAttack();
        }
    }
    public override void TakeDamage(float amount)
    {
        if (currentState == HomerState.Idle || currentState == HomerState.Attack)
        {
            health -= amount;
            if (health < 0)
            {
                Death();
            }
            currentState = HomerState.Hurt;
            sprite.color = Color.red;
            StartCoroutine(HurtCoroutine());
        }
    }

    private IEnumerator HurtCoroutine()
    {
        yield return new WaitForSeconds(hurtCooldown);
        currentState = HomerState.Attack;
        sprite.color = Color.white;
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

    public override void Death()
    {
        currentState = HomerState.Death;
        sprite.color = Color.black;
        StartCoroutine(DeathCoroutine());
    }

    private IEnumerator DeathCoroutine()
    {
        yield return new WaitForSeconds(deathTime);
        Destroy(gameObject);
    }

    public override void DisableAllAttacks()
    {
        currentState = HomerState.Idle;
    }

    protected override void WaitForPlayerToRecover(float amt)
    {
        throw new System.NotImplementedException();
    }
}


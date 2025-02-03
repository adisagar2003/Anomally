using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour, IDamagable
{
    [SerializeField] protected float health = 10.0f;
    [SerializeField] protected float damage = 10.0f;
    [SerializeField] protected float attackCooldown  = 0.5f;
    [SerializeField] protected float hurtCooldown  = 1.2f;

    protected void OnEnable()
    {
        Player.DeathEvent += DisableAllAttacks;
    }

    protected void OnDisable()
    {
        Player.DeathEvent -= DisableAllAttacks;
    }

    public virtual void TakeDamage(float amount)
    {
        
    }

    public abstract void Attack();
    public abstract void Damage(float damageAmount);

    public abstract void DisableAllAttacks();
    public virtual void Death()
    {

    }
}

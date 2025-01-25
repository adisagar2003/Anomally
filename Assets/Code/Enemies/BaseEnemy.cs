using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour, IDamagable
{
    public float health { get; protected set; } = 10.0f;
    public float damage { get; protected set; } = 10.0f;
    public float attackCooldown { get; protected set; } = 0.5f;

    public virtual void TakeDamage(float amount)
    {
        
    }

    public abstract void Attack();
    public abstract void Damage(float damageAmount);
    public virtual void Death()
    {

    }
}

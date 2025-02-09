using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public float health  = 100.0f;
    [SerializeField] private float damage = 3.0f;
    [SerializeField] private float attackCooldown = .4f;
    private Player player;

    [Header("Attack")]
    [SerializeField] private GameObject attackColliderPosition;
    [SerializeField] private float attackColliderRadius;
    private bool isAttacking = false;

    // event: Enemy damaged -> Camera Shake
    public delegate void EnemyDamaged();
    public static event EnemyDamaged EnemyDamagedEvent;

    private void OnEnable()
    {
        Player.DamageEvent += TakeDamage;
    }
    private void Start()
    {
        player = GetComponent<Player>();
    }

    public void Attack()
    {
        // make a colldier
        Collider2D[] obj = Physics2D.OverlapCircleAll(attackColliderPosition.transform.position, attackColliderRadius);

        foreach(Collider2D ob in obj)
        {
            if (ob.tag == "Destructible")
            {
                IDamagable obInterface = ob.GetComponent<IDamagable>();
                if (obInterface != null) {
                    obInterface.TakeDamage(damage);
                    EnemyDamagedEvent();
                }
            }
            
        }
        StartCoroutine(AttackCoroutine());
    }

    private void OnDrawGizmos()
    {
        if (player.currentState == Player.PlayerState.Attack)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackColliderPosition.transform.position, attackColliderRadius);
        }
    }

    private void TakeDamage(float damage)
    {
        health -= damage;
    }

    private IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(attackCooldown);
        player.currentState = Player.PlayerState.Idle;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public float health  = 100.0f;
    [SerializeField] private float damage = 3.0f;
    [SerializeField] private float attackCooldown = .4f;
    [SerializeField] private float sparkTimer = .4f;
    private Player player;

    [Header("Attack")]
    [SerializeField] private GameObject attackColliderPosition;
    [SerializeField] private float attackColliderRadius;
    private bool isAttacking = false;

    [Header("FX")]
    [SerializeField] private GameObject swordFX;
    private GameObject swordFXRef;

    // event: Enemy damaged -> Camera Shake
    public delegate void EnemyDamaged();    
    public static event EnemyDamaged EnemyDamagedEvent;

    private void OnEnable()
    {
        Player.PlayerDamageEvent += TakeDamage;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
    private void Start()
    {
        player = GetComponent<Player>();
    }

    public float GetAttackCooldown()
    {
        return attackCooldown;
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

           if (ob.tag == "RunnerHurtCollider")
            {
                Runner runnerRef = ob.GetComponentInParent<Runner>();
                if (runnerRef != null)
                {
                    runnerRef.GetHurtByPlayer(player.GetFacingDirection());
                    EnemyDamagedEvent();
                }
            }
           
           if (ob.tag == "HurtCollider")
            {
                
                EnemyDamagedEvent();
                BaseEnemy enemyRef = ob.GetComponentInParent<BaseEnemy>();
                if (enemyRef != null)
                {
                    GameObject swordFXSpawned = Instantiate(swordFX, ob.transform);
                    enemyRef.transform.SetParent(swordFXSpawned.transform);
                    // Save ref for coroutine
                    swordFXRef = swordFXSpawned;
                    swordFXSpawned.transform.localPosition = new Vector3(0, 0, 0);
                    StartCoroutine(DestroySpark());
                    enemyRef.TakeDamage(damage);
                }
            }
            
        }
    }

    private IEnumerator DestroySpark()
    {
        yield return new WaitForSeconds(sparkTimer);

        Destroy(swordFXRef);
    }

    private void OnDrawGizmos()
    {
        if (player == null) return;
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
}

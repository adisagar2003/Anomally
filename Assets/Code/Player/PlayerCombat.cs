using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{   
    [SerializeField] public float health { get; private set; } = 100.0f;
    [SerializeField] private float damage = 3.0f;
    [SerializeField] private float attackCooldown = .4f;
    private Player player;


    [Header("Attack")]
    [SerializeField] private GameObject attackColliderPosition;
    [SerializeField] private float attackColliderRadius;
    private bool isAttacking = false;

    private void Start()
    {
        player = GetComponent<Player>();
    }
    public void Attack()
    {
        // make a colldier
        Collider2D[] obj = Physics2D.OverlapCircleAll(attackColliderPosition.transform.position, attackColliderRadius);

        // Future: Implement destructible;
        foreach(Collider2D ob in obj)
        {
            if (ob.tag == "Destructible")
            {
                IDamagable obInterface = ob.GetComponent<IDamagable>();
                if (obInterface != null) {
                    Debug.Log("Damage given to object");
                    obInterface.TakeDamage(damage); 
                }

            }
            
        }
        // make it dissapear after the coroutine ends 
        StartCoroutine(AttackCoroutine());
        // make player to idle again after waiting

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackColliderPosition.transform.position, attackColliderRadius);
    }

    private IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(attackCooldown);
        player.currentState = Player.PlayerState.Idle;
    }
}

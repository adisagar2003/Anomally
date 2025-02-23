using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RihnoHurtbox : MonoBehaviour
{
    private Rihno rihno;
    public bool isInAttackArea = false;
    private Vector2 directionOfAttack;
    private void OnDisable()
    {
        StopAllCoroutines();
    }
    private void Start()
    {
        rihno = GetComponentInParent<Rihno>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isInAttackArea = true;
        if (rihno == null) return;
        if (rihno.rihnoStateMachine.currentState != rihno.rihnoAttackState) AttackPlayerIfCollision(collision);
    }

    private void AttackPlayerIfCollision(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player playerRef = collision.GetComponent<Player>();
            if (playerRef == null) return;
            DirectionTowardsTarget(playerRef);
            rihno.Attack(GetDirectionOfAttack());
        }
    }

    private void DirectionTowardsTarget(Player playerRef)
    {
        Vector2 directionOfAttack = (playerRef.transform.position - rihno.transform.position).normalized;
        this.directionOfAttack = directionOfAttack;
    }

    public Vector2 GetDirectionOfAttack()
    {
        return directionOfAttack;
    }
    private IEnumerator AttackAgainIfInReigon()
    {
        
        yield return new WaitForSeconds(1.0f);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isInAttackArea = false;
    }
}

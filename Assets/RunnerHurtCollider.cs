using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerHurtCollider : MonoBehaviour
{
    private Runner runner;
    private Player player;

    private void Start()
    {
        player = FindFirstObjectByType<Player>();
        runner = GetComponentInParent<Runner>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // direction of hurt;
            
            Vector2 directionOfHurt = (player.transform.position - transform.position).normalized;
            // send a signal to runner that it hurt the player
            if (runner.flipped == true)
            {
                Debug.Log("Give damage to player === RunnerHurtCollider.cs");
                runner.GiveDamageToPlayer(directionOfHurt);
            }
            else
            {
                Debug.Log("Give damage to player === RunnerHurtCollider.cs on the flipped");
                runner.GiveDamageToPlayer(directionOfHurt);
            }
        }
    }
}

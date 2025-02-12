using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerHurtCollider : MonoBehaviour
{
    private Runner runner;
    private Player player;
    private RunnerMovement runnerMovement;

    private void Start()
    {
        player = FindFirstObjectByType<Player>();
        runner = GetComponentInParent<Runner>();
        runnerMovement = GetComponentInParent<RunnerMovement>();
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
                runner.GiveDamageToPlayer(directionOfHurt);
                // stop the player 
                runnerMovement.StopRunner();
            }
            else
            {
                runner.GiveDamageToPlayer(directionOfHurt);
                runnerMovement.StopRunner();
            }
        }
    }
}

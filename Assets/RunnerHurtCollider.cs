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
            Vector2 directionOfHurt = (transform.position - player.transform.position).normalized;
            // send a signal to runner that it hurt the player
            runner.GiveDamageToPlayer(directionOfHurt);
        }
    }
}

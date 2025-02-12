using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerHitCollider : MonoBehaviour
{
    private bool isInAttackArea;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInAttackArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInAttackArea = false;
        }
    }
}

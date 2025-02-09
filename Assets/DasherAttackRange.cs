// Uses the box collider for player detection
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DasherAttackRange : MonoBehaviour
{
    private Dasher dasher;
    private void Awake()
    {
        dasher = FindFirstObjectByType<Dasher>();        
    }
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            dasher.StartAttack(collision.transform.position);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }

}

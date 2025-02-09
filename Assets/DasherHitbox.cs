using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DasherHitbox : MonoBehaviour
{
    private Player player;
    private Dasher parentDasher;
    // Start is called before the first frame update
    void Start()
    {
        parentDasher = GetComponentInParent<Dasher>();
        player = FindFirstObjectByType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (player.currentState != Player.PlayerState.Attack)
            {
                player.TakeDamage();
            } 
        }
    }
}

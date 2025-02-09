using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DasherHurtBox : MonoBehaviour
{
    private Player player;
    private Dasher dasher;
    private DasherMovement dasherMovement;
    private void Start()
    {
        player = FindFirstObjectByType<Player>();
        dasher = GetComponentInParent<Dasher>();
        dasherMovement = GetComponentInParent<DasherMovement>();
    }
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player playerReference = collision.gameObject.GetComponent<Player>();
            if (playerReference == null) Debug.LogError("The reference retured null"); 
            else if (playerReference.currentState == Player.PlayerState.Attack)
            {
                Debug.Log("Dasher will get hurt");
                // future implementation
                dasher.TakeDamage(4f);
                // get hurt
                // knockback
                Vector2 knockbackDirection = (transform.position - player.transform.position).normalized;
                dasherMovement.Knockback(knockbackDirection);
            }
        }
    }
}

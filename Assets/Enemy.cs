using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    // Start is called before the first frame update

    public float health = 19.0f;
    private Animator animator;
    GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        IsFacingObject(); 
    }


    private bool IsFacingObject()
    {
        // Check if the gaze is looking at the front side of the object
        Vector2 forward = transform.forward;
        Vector2 toOther = (player.transform.position - transform.position).normalized;

        if (Vector2.Dot(forward, toOther) < 0.7f)
        {
            Debug.Log("facing the object");
            transform.rotation = Quaternion.Euler(0, 180, 0);
            return true;
        }

        Debug.Log(" Not Facing the object");
        return false;
    }

    public void LookTowardsPlayer()
    {
        
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health < 0)
        {
            Destruct();
        }
    }

    public void Destruct()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            animator.SetBool("IsInRange", true);
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RihnoHurtbox : MonoBehaviour
{
    private Rihno rihno;

    private void Start()
    {
        rihno = GetComponentInParent<Rihno>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (rihno == null) return;
        if (collision.CompareTag("Player"))
        {
            Player playerRef = collision.GetComponent<Player>();
            if (playerRef == null) return;
            Vector2 directionOfAttack = (playerRef.transform.position - rihno.transform.position).normalized;

            rihno.Attack(directionOfAttack, true);
        }
    }
}

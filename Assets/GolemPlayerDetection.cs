using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemPlayerDetection : MonoBehaviour
{
    // Start is called before the first frame update
    private Golem golem;
    void Awake()
    {
        golem = GetComponentInParent<Golem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (golem == null) return;
        if (!collision.CompareTag("Player")) return; 
        golem.SetInDetectableArea(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //if (golem == null) return;
        if (!collision.CompareTag("Player")) return;
        golem.SetInDetectableArea(false);
    }

    private void Update()
    {
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerPlayerDetector : MonoBehaviour
{
    private Runner runner;
    // Start is called before the first frame update
    void Start()
    {
        runner = FindFirstObjectByType<Runner>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            runner.SetIsDetectableArea(true);
            runner.Attack(collision.transform.position);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            runner.SetIsDetectableArea(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
            
    }
}

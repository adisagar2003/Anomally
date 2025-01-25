using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleBox : MonoBehaviour, IDamagable
{
    [Header("Health")]
    [SerializeField] private float health = 10.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        
        if (health < 0)
        {
            Death();
        }
    }

    public void Death()
    {
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

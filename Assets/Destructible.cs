using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour, IDamagable
{
    [SerializeField] float health = 4.0f;
    public void Destruct()
    {
        Destroy(gameObject);
    }
    public void TakeDamage(float amount)
    {
        health = health - amount;
        if (health < 0)
        {
            Destruct();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

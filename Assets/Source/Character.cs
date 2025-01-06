using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // This is the parent script for all characters (Player, Enemies);

    [SerializeField] protected float health = 100.0f;
    [SerializeField] protected float speed = 10.0f;
    [SerializeField] protected float attackPower = 3;
 
    private WaitForSeconds deathDelay = new WaitForSeconds(2);





    protected virtual void TakeDamage(float amount)
    {

        health -= amount;
        
        if (health < 0)
        {
            StartCoroutine(Death());
        }
    }

    protected virtual IEnumerator Death()
    {
       
        yield return deathDelay;
        Destroy(gameObject);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}

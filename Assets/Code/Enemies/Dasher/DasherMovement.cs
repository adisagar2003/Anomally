using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DasherMovement : MonoBehaviour
{
    private Player player;
    private Rigidbody2D rb2D;
    private float direction = -1;
    [SerializeField] private float speed = 10;
    [SerializeField] private float recoilSpeed= 10;

    // Start is called before the first frame update
    void Start()
    {
        player = FindFirstObjectByType<Player>();
        rb2D = GetComponent<Rigidbody2D>();
    }
    private float time = 0;
    // Update is called once per frame

    private void FixedUpdate()
    {
        
    }



    public void ChasePlayer()
    {
        Vector2 targetPosition = new Vector2(player.transform.position.x, transform.position.y);
        //Vector2 newPosition = Vector2.MoveTowards(rb2D.position, targetPosition, speed * Time.deltaTime);
        Vector2 direction = (targetPosition - new Vector2(transform.position.x, transform.position.y).normalized);
        rb2D.velocity = direction * speed;
        
    }

    public void RecoilBack()
    {
        // calculate the direction it is in 
        int direction = 1;
        if (player.transform.rotation.y == 180) direction = 1;
        if (player.transform.rotation.y == 0) direction = -1;
        rb2D.AddForce(new Vector2(player.transform.rotation.x * direction, 0) * recoilSpeed, ForceMode2D.Impulse);
    }


    private void HandleFlip()
    {
       
        if (rb2D.velocity.x > 0)
        {
            player.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (rb2D.velocity.x < 0)
        {
            player.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    void Update()
    {
        HandleFlip();
    }
}

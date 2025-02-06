using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DasherMovement : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private float direction = -1;
    [SerializeField] private float speed = -1;
    private Player player;
    private Quaternion lookRotation;
    // Start is called before the first frame update
    void Start()
    {
        player = FindFirstObjectByType<Player>();
        rb2D = GetComponent<Rigidbody2D>();
    }
    private float time = 0;
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if ((int) time % 2  == 0)
        {
            lookRotation = Quaternion.LookRotation(player.transform.position);
        }
        else
        {
            direction = 1;
            rb2D.velocity = new Vector2(-direction * speed, 0);
        }

    }
}

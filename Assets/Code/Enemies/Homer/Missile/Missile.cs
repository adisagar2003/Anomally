using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public enum MisslieState
    {
        Spawn,
        Locked,
        Thrown
    }
    // Start is called before the first frame update
    [SerializeField] private float upwardForce;
    private Rigidbody2D rb2D;
    [SerializeField] private float rocketSpeed = 1.0f;
    [SerializeField] private float gravity  = 24.0f;
    [SerializeField] private Player player;
    [SerializeField] private float spawnTime;
    private Vector2? lookTowardsDirection = null;
    [SerializeField] private MisslieState currentState;
    private void Awake()
    {
        currentState = MisslieState.Spawn;
        rb2D = GetComponent<Rigidbody2D>();
        player = FindFirstObjectByType<Player>();
    }
    void Start()
    {
        StartCoroutine(DeathByTime());
        rb2D.AddForce(Vector2.up * upwardForce, ForceMode2D.Impulse);
        rb2D.gravityScale = gravity;
        StartCoroutine(SpawnToLockedState());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
        if (MisslieState.Locked == currentState)
        {
            // lock in a specific target;
            if (lookTowardsDirection == null)
            {
                lookTowardsDirection = (player.transform.position - transform.position).normalized;
            }

            rb2D.velocity = (Vector2) (lookTowardsDirection * rocketSpeed);

        }
    }

    private IEnumerator SpawnToLockedState()
    {

        yield return new WaitForSeconds(spawnTime);
        if (currentState == MisslieState.Spawn)
        {
            currentState = MisslieState.Locked;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            // Future feat: explosion
            Destroy(gameObject);
        }

        if (collision.tag == "Player")
        {
            
            Destroy(gameObject);
        }
    }

    private IEnumerator DeathByTime()
    {
        yield return new WaitForSeconds(4.0f);
        Destroy(gameObject);
    }
}

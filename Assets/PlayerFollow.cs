using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Assigned to a gameObject that follows the x and y of the Current player.
public class PlayerFollow : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Player player;
    void Start()
    {
        player = GameObject.FindFirstObjectByType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(player.transform.position.x, player.transform.position.y);

    }
}
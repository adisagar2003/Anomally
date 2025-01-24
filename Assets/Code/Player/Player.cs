using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerMovement playerMovement;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MovePlayer(float xInput)
    {
        playerMovement.HandleMovement(xInput);

    }

    public void Jump()
    {
        playerMovement.Jump();
    }
}

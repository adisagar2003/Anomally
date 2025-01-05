using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character
{
    InputControls controls;
    // Start is called before the first frame update
    private Rigidbody2D rb2D;

    // x axis controller input 
    float xDirection;
    float yDirection;

    // gravity 
    [SerializeField]
    // adding gravity
    private float gravityScale = -10f;


    // Character movement
    private void CharacterMovement()
    {
        if (xDirection < 0)
        {
            rb2D.velocity = new Vector2(-speed, gravityScale);
        }
        else if (xDirection > 0)
        {
            rb2D.velocity = new Vector2(speed, gravityScale);
        }
        else
        {
            rb2D.velocity =new Vector2(0,gravityScale);
        }
    }

    // Handle sprite flip
    protected void HandleSpriteFlip()
    {
        if (rb2D.velocity.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (rb2D.velocity.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private void Awake()
    {
        GetInput();
    }

    // Enable gameplay controls

    void OnEnable()
    {
        controls.PlayerControls.Enable();
    }

    void OnDisable()
    {
        controls.PlayerControls.Disable();    
    }
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CharacterMovement();
        HandleSpriteFlip();
    }


    void AddGravity()
    {
        
    }

    private void GetInput()
    {
        controls = new InputControls();

        // take some horizontal input.
        controls.PlayerControls.Horizontal.performed += ctx => xDirection = ctx.ReadValue<float>(); ;
        controls.PlayerControls.Horizontal.canceled += ctx => xDirection = 0;

        // take some vertical input;
        controls.PlayerControls.CameraControl.performed += ctx => yDirection = ctx.ReadValue<float>();
        controls.PlayerControls.CameraControl.canceled += ctx => yDirection = 0;
    }
}

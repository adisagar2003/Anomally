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


    // Raycast data
    [SerializeField] private float raycastDirection = 5.0f;
    [SerializeField] private float raycastLength = 10.0f;
    [SerializeField] private GameObject eye;
    // Camera Follow Reference


    // Animation 
    private Animator playerAnimator;

    // gravity 
    [SerializeField]
    // adding gravity
    private float gravityScale = -10f;

    // facing direction
    float facingDirection = 0f;

    

    // combat functionality
    bool attackAttempt;


    //--------------------------DEBUGGING PURPOSES ONLY `````````````````//
    [Header("Debug :3")]
    [SerializeField] private float attackCooldown = 0.8f;
    [SerializeField] private float sphereRadius = 0.3f;
    [SerializeField] private Vector3 hitColliderOffset;
    //Implementing state machine
    public enum PlayerState
    {
        Idle,
        Run,
        Attack,
        Dead,
        Hurt
    }

    private PlayerState _state;

    // Character movement
    private void CharacterMovement()
    {
        // Player moves if and only if in idle and run state.
        if (_state == PlayerState.Hurt || _state == PlayerState.Dead || _state == PlayerState.Attack)
        {
            // keep the body in idle
            rb2D.velocity = new Vector2(0, gravityScale);
            return;
        };

        if (xDirection < 0)
        {
            rb2D.velocity = new Vector2(-speed, gravityScale);
            _state = PlayerState.Run;
            facingDirection = -1;
        }
        else if (xDirection > 0)
        {
            rb2D.velocity = new Vector2(speed, gravityScale);
            _state = PlayerState.Run;
            facingDirection = 1;
        }
        else
        {
            // Change to idle only if not hurt or dead
            if (_state == PlayerState.Run) _state = PlayerState.Idle;
            rb2D.velocity = new Vector2(0, gravityScale);
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
    // START
    void Start()
    {
        _state = PlayerState.Idle;
        rb2D = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
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

    // Update is called once per frame
    void Update()
    {
        CharacterMovement();
        HandleSpriteFlip();
        EyeRay();
        AnimHandle();
    }
  
    // for debugging
    private void OnDrawGizmos()
    {
        Debug.DrawRay(eye.transform.position, Vector2.right * facingDirection * raycastLength, Color.red);
        Gizmos.DrawWireSphere(transform.position + hitColliderOffset,sphereRadius);
    }
    private void EyeRay()
    {
        RaycastHit2D hit = Physics2D.Raycast(eye.transform.position, Vector2.left * facingDirection * raycastLength);

        if (hit)
        {
            if (hit.collider.tag == "EndTilemap")
            {
                // Change the camera settings to static

            }
        }
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

        // take attack input
        controls.PlayerControls.Attack.started += ctx => AttemptAttack();

    }


    /* -------------------- ANIMATION ------------------------------------*/
    private void AnimHandle()
    {
        // idle when not moving
        if (_state == PlayerState.Idle)
        {
            playerAnimator.SetInteger("AnimState", 0);
        }
        else if (_state == PlayerState.Run)
        {
            playerAnimator.SetInteger("AnimState", 2);
        }
        else if (_state == PlayerState.Attack)
        {
            
        }
        
    }

    /* -------------------- MELEE COMBAT---------------------------------*/
    private void AttemptAttack()
    {
        // check if attemptAttack is false;
        // if false, check for state, is it not hurting or dead
        // if not start a coroutine which makes attemptAttack false and changes the state to attack
        if (attackAttempt == false && _state != PlayerState.Hurt && _state != PlayerState.Dead && _state != PlayerState.Attack) 
        {
            StartCoroutine(AttackingOnTime());
        }
    }

    // coroutine for attack attempt.
    private IEnumerator AttackingOnTime()
    {
        playerAnimator.SetTrigger("Attack");
        Physics2D.OverlapCircleAll(transform.position + hitColliderOffset, sphereRadius);
        _state = PlayerState.Attack;
        yield return new WaitForSeconds(attackCooldown);
        attackAttempt = false;
        _state = PlayerState.Idle;

    }
}


  
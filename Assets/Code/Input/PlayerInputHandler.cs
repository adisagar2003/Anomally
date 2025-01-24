using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public InputMaster controls;
    public Player player;
    public Vector2 xInput;
    
    
    void Awake()
    {
        controls = new InputMaster();
        controls.PlayerControls.Move.performed += ctx => xInput = ctx.ReadValue<Vector2>();
        controls.PlayerControls.Move.canceled += ctx => xInput = Vector2.zero;
        controls.PlayerControls.Jump.performed += ctx => JumpAction();
       
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalInput(xInput);
    }

    // returns the x value
    void HorizontalInput(Vector2 inputValue)
    {
        player.MovePlayer(inputValue.x);
    }

    void JumpAction()
    {
        player.Jump();
    }

    private void OnEnable()
    {
        controls.PlayerControls.Enable();
    }

    private void OnDisable()
    {
        controls.PlayerControls.Disable();
    }
}

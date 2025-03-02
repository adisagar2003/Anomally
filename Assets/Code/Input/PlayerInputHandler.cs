using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public InputMaster controls;
    public Player player;
    public Vector2 xInput;

    public enum InputSystem
    {
        Enable,
        Disable
    }

    public InputSystem currentState;
    void Awake()
    {
        player = FindFirstObjectByType<Player>();
        controls = new InputMaster();
    }

    public void AttackAction()
    {
        if (currentState == InputSystem.Disable) return;
        player.Attack();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == InputSystem.Disable) return;
        HorizontalInput(xInput);
    }

    // returns the x value
    void HorizontalInput(Vector2 inputValue)
    {
        if (currentState == InputSystem.Disable) return;
        player.MovePlayer(inputValue.x);
    }

    void JumpAction()
    {
        if (currentState == InputSystem.Disable) return;
        player.Jump();
    }

    void DashAction()
    {
        if (currentState == InputSystem.Disable) return;
        player.Dash();
    }
    private void OnEnable()
    {

        controls.PlayerControls.Move.performed += ctx => xInput = ctx.ReadValue<Vector2>();
        controls.PlayerControls.Move.canceled += ctx => xInput = Vector2.zero;
        controls.PlayerControls.Jump.performed += ctx => JumpAction();
        controls.PlayerControls.Dash.performed += ctx => DashAction();
        controls.PlayerControls.Fire.performed += ctx => AttackAction();
        controls.PlayerControls.Enable();
    }

    public void OnDisable()
    {
        controls.PlayerControls.Move.performed -= ctx => xInput = ctx.ReadValue<Vector2>();
        controls.PlayerControls.Move.canceled -= ctx => xInput = Vector2.zero;
        controls.PlayerControls.Jump.performed -= ctx => JumpAction();
        controls.PlayerControls.Dash.performed -= ctx => DashAction();
        controls.PlayerControls.Fire.performed -= ctx => AttackAction();
        controls.PlayerControls.Disable();
    }

    // Use case: disable all input when doing some actions
    public void EnableInput()
    {
        currentState = InputSystem.Enable;
    }

    public void DisableInput()
    {
        // set all the existing values to 0
        currentState = InputSystem.Disable;
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterFollowPoint : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform CharacterPosition;
    public Transform CameraPosition;
    public float speed = 20f;
    public Vector3 offset;
    public float leanTowardsDirection;
    public float CameraBoundsX;
    // get input
    InputControls controls;
    float xDirection;
    public enum CameraState
    {
        FollowPlayer, 
        Static
    }

    public CameraState _state;
  
    private void Awake()
    {
        
        GetInput();
    }

    void Start()
    {

        _state = CameraState.FollowPlayer;
    }


    // essential for control system enabling 
    void OnEnable()
    {
        controls.PlayerControls.Enable();
    }

    void OnDisable()
    {
        controls.PlayerControls.Disable();
    }

    private void GetInput()
    {
        controls = new InputControls();

        // take some horizontal input.
        controls.PlayerControls.Horizontal.performed += ctx => xDirection = ctx.ReadValue<float>(); ;
        controls.PlayerControls.Horizontal.canceled += ctx => xDirection = 0;

        // take some vertical input;
    }

    // Update is called once per frame

    void FixedUpdate()
    {
        // according to states
        if (_state == CameraState.FollowPlayer) 
        {
            LerpCamera();
        }
        
    }


    // Lerp camera according to player's position
    void LerpCamera()
    {
        if (xDirection < 0)
        {
            // Adding the values while clamping    to prevent the camera to go out of bonds
            Debug.Log("Should lean towards the negative");
            Vector3 newPosition = CharacterPosition.position + new Vector3(leanTowardsDirection, 0, 0);
            newPosition = new Vector3(Mathf.Clamp(newPosition.x, CameraBoundsX, 10000000),  newPosition.y, newPosition.z);
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * speed) + offset;

        }

        else if (xDirection > 0)
        {
            // Adding the values while clamping to prevent the camera to go out of bounds
            Vector3 newPosition = CharacterPosition.position + new Vector3(-leanTowardsDirection, 0, 0);
            newPosition = new Vector3(Mathf.Clamp(newPosition.x, CameraBoundsX, 10000000), newPosition.y, newPosition.z);
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * speed) + offset;
        }

        else if (xDirection == 0)
        {
            // Adding the values while clamping to prevent the camera to go out of bounds
            Vector3 newPosition = CharacterPosition.position;
            newPosition = new Vector3(Mathf.Clamp(newPosition.x, CameraBoundsX, 10000000), newPosition.y, newPosition.z);
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * speed) + offset;

        }
    }
}

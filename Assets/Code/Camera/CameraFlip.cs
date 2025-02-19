using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraFlip : MonoBehaviour
{
    // Start is called before the first frame update
    private CinemachineVirtualCamera cinemachineVirtualCamera;

    private CinemachineFramingTransposer framingTransposer;
    [SerializeField] private Player player;
    private Vector3 offset;
    // to prevent too many calls
    private float facingDirection = 1;
    [SerializeField] string debugString = "";
    void Start()
    {
        cinemachineVirtualCamera = GetComponentInParent<CinemachineVirtualCamera>();

        framingTransposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        // store offset;
        offset = framingTransposer.m_TrackedObjectOffset;
    }

    private void OnEnable()
    {
        PlayerMovement.FlipEvent += FlipCamera;   
    }

    private void OnDisable()
    {
        PlayerMovement.FlipEvent -= FlipCamera;
    }


    // gets called whenever the player switched direction
    public void FlipCamera(float direction)
    {
        if (cinemachineVirtualCamera == null) return;
        if (direction == 0) return;
        if (direction == facingDirection) return;
        if (direction != facingDirection)
        {
            facingDirection = direction;
            if (player == null) return;
            offset.x = offset.x * direction;
            framingTransposer.m_TrackedObjectOffset = offset;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

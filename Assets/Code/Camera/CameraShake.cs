using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float shakeStrength;
    [SerializeField] private float shakeCooldown = 1.0f;
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;

    private float shakeTime = 0;

    private void OnEnable()
    {
        PlayerCombat.EnemyDamagedEvent += ShakeCamera;
    }


    public enum CameraState
    {
        Idle,
        Shake,
        Static
    }

    private CameraState currentState;

    private void Awake()
    {
        currentState = CameraState.Idle;
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        shakeTime = 0;
    }

    private void Update()
    {
        if (shakeTime/0.01 > 1)
        {
            shakeTime -= Time.deltaTime * shakeCooldown;
        }
        
        else 
        {

            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
            currentState = CameraState.Idle;
        }
    }

    [ContextMenu("Shake Camera")]
    public void ShakeCamera()
    {
        if (currentState != CameraState.Idle) return;
        currentState = CameraState.Shake;
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 1;
        shakeTime = 0.9f;
    }

}

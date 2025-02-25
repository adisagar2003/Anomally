using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSendDeathSignal : MonoBehaviour
{
    private Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }
    
    public void SendDeathEvent()
    {
        player.InvokeDeathEvent();
    }
    
    public void SendAttackSignal()
    {
        player.Attack();
    }
}

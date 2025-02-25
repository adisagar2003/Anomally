using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
public class GolemWalkState : GolemState
{

    private Player playerRef;
    public GolemWalkState(Golem golemRef, GolemStateMachine golemStateMachine) : base(golemRef, golemStateMachine)
    {
        currentStateName = "Walk";
    }
    public override void EnterState()
    {
        playerRef = GameObject.FindFirstObjectByType<Player>();
        
        golemAnimator.SetBool("isWalking", true);
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override void ExitState()
    {
        golemAnimator.SetBool("isWalking", false);
      
        base.ExitState();
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string GetStateName()
    {
        return base.GetStateName();
    }

    public override void OnFixedUpdate()
    {
        Vector2 directionTowardsPlayer = Vectors.FindDirectionTowardsPlayer(playerRef, golem.transform.position);
        golem.MoveGolem(directionTowardsPlayer);
        float distanceFromPlayer = Vector2.Distance(playerRef.transform.position, golem.transform.position);
        if (distanceFromPlayer < 2.3f)
        {
            golemStateMachine.ChangeState(golem.golemAttackState);
        }
        base.OnFixedUpdate();
    }


   
    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override string ToString()
    {
        return base.ToString();
    }
}

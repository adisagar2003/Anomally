using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RihnoDeathState : RihnoState
{
    private float deathCooldown;
    private float timer = 0;
    public RihnoDeathState(Rihno rihnoRef, RihnoStateMachine rihnoStateMachine,float deathCooldown) : base(rihnoRef,  rihnoStateMachine)
    {
        this.deathCooldown = deathCooldown;
    }
    public override void EnterState()
    {
        base.EnterState();
        rihnoAnimator.SetBool("isDead", true);
        rihno.DisableAllAttacks();
    }

    public override bool Equals(object obj)
    {
        return true;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override void OnFixedUpdateState()
    {
        base.OnFixedUpdateState();
    }

    public override void OnUpdateState()
    {
        timer += Time.deltaTime;
        if (timer > deathCooldown)
        {
            rihno.DestroyObject();
        }
    }

    public override string ToString()
    {
        return base.ToString();
    }
}

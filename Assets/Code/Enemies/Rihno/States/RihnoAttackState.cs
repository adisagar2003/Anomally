using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RihnoAttackState : RihnoState
{
    [SerializeField] private float rihnoAttackCooldown = 1.0f;
    [SerializeField] private RihnoHurtbox rihnoHurtbox;
    private float timer = 0;
    public RihnoAttackState(Rihno rihnoref, RihnoStateMachine rihnoStateMachine) : base(rihnoref, rihnoStateMachine)
    {
        this.rihno = rihnoref;
        this.rihnoStateMachine = rihnoStateMachine;
    }

    private void a()
    {
        // this will be needed to check if the object is in attack range after attack is executed
    }
    public override void EnterState()
    {
        timer = 0.0f;
        rihnoHurtbox = rihno.GetComponentInChildren<RihnoHurtbox>();
        base.EnterState();
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
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

        if (timer > rihnoAttackCooldown)
        {
            if (rihnoHurtbox == null) rihnoStateMachine.ChangeState(rihno.rihnoIdleState);

            // Attack again if in attack range, chase otherwise
            if (rihnoHurtbox.isInAttackArea)
            {
                rihno.Attack(rihnoHurtbox.GetDirectionOfAttack());
            }
            else
            {
                rihnoStateMachine.ChangeState(rihno.rihnoChaseState);
            }
            
        }
    }

    public bool GetIsInAttackArea()
    {
        return rihnoHurtbox.isInAttackArea;
    }

    public override string ToString()
    {
        return base.ToString();
    }

  
}

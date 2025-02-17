using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RihnoAttackState : RihnoState
{
    [SerializeField] private float rihnoAttackCooldown = 1.0f;
    private float timer = 0;
    public RihnoAttackState(Rihno rihnoref, RihnoStateMachine rihnoStateMachine) : base(rihnoref, rihnoStateMachine)
    {
        this.rihno = rihnoref;
        this.rihnoStateMachine = rihnoStateMachine;
    }

    public override void EnterState()
    {
        timer = 0.0f;
        base.EnterState();
        Debug.Log("Entering Attack State");
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
            Debug.Log("Back to Chase state");
            rihnoStateMachine.ChangeState(rihno.rihnoChaseState);
        }
    }

    public override string ToString()
    {
        return base.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

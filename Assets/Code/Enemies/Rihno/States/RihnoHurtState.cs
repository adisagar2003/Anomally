using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RihnoHurtState : RihnoState
{
    [SerializeField] private float hurtCooldown = 1.2f;
    private float timer = 0.0f;
    public RihnoHurtState(Rihno rihnoref, RihnoStateMachine rihnoStateMachine) : base(rihnoref, rihnoStateMachine)
    {
    }

    public override void EnterState()
    {
        timer = 0.0f;
        Debug.Log("Entered Hurt State");
        rihnoAnimator.SetTrigger("Hurt");
       
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
        if (timer > hurtCooldown)
        {
           
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

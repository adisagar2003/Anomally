using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RihnoChaseState : RihnoState
{
    private Transform playerTransform;
    public RihnoChaseState(Rihno rihnoref, RihnoStateMachine rihnoStateMachine) : base(rihnoref, rihnoStateMachine)
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        this.rihno = rihnoref;
        this.rihnoStateMachine = rihnoStateMachine;
    }

    public override void EnterState()
    {
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
        Vector2 directionTowardsPlayer = (playerTransform.position - rihno.transform.position).normalized;
        directionTowardsPlayer.y = 0;
        rihno.MoveRihno(directionTowardsPlayer);
        base.OnFixedUpdateState();
    }

    public override void OnUpdateState()
    {
        
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

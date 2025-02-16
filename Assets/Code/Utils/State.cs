using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    public bool isCompleted { get; protected set; }
    protected float startTime;
    public float time => Time.time - startTime;
    public virtual void Start()
    {
        // Start state
    }

    public virtual void End()
    {
        // End state
    }

    public virtual void OnUpdate()
    {
        // this part runs inside Update();
    }

    public virtual void OnFixedUpdate()
    {
        // this part runs inside FixedUpdate();
    }
}

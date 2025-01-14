using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Subject : MonoBehaviour
{
    // Stores a list of observers
    [Header("Observers")]
    [SerializeField] protected List<IObserver> observers = new List<IObserver>();
    
    // invoke event 
    public void AddObserver(IObserver obs)
    {
        observers.Add(obs);
    }

    public void RemoveObserver(IObserver obs)
    {
        observers.Remove(obs);
    }

    protected void InvokeEvent()
    {
        observers.ForEach((IObserver observer) =>
        {
            observer.InvokeEvent();
        });
    }
    
}

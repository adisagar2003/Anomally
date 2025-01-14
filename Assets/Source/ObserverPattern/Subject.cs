using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Subject : MonoBehaviour
{
    // Stores a list of observers
    [Header("Observers")]
    [SerializeField] public List<IObserver> observers = new List<IObserver>();
    
    // invoke event 
    protected void AddObserver(IObserver obs)
    {
        observers.Add(obs);
    }

    protected void RemoveObserver(IObserver obs)
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

using System;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    private Soldier _soldier;
    protected Soldier soldier {
        get {
            if(!_soldier)
                _soldier = GetComponentInParent<Soldier>();
            return _soldier;
        }
    }

    virtual protected List<AIBehaviour> behaviours => new List<AIBehaviour>();

    protected AIBehaviour ActiveBehaviour { get => behaviours.Find(item => item.IsActivated()); }

    internal void DisableAllBehaviour()
    {
        behaviours.ForEach(behaviour => behaviour.Deactivate());
    }

    internal void EnableEntry()
    {
        if(behaviours.Count > 0)
            SetBehaviour(behaviours[0]);
    }

    internal void SetBehaviour(AIBehaviour behaviour)
    {
        if(!behaviours.Contains(behaviour))
            return;
        DisableAllBehaviour();
        behaviour.Activate();
    }

    public virtual void OnCollisionEnter(Collision other){
        ActiveBehaviour?.OnCollisionEnter(other);
    }

    internal virtual bool IsInactive()
    {
        return false;
    }
}
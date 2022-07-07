using System;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    private AIBehaviour activeBehaviour;
    private Soldier _soldier;
    protected Soldier soldier {
        get {
            if(!_soldier)
                _soldier = GetComponentInParent<Soldier>();
            return _soldier;
        }
    }

    virtual protected List<AIBehaviour> behaviours => new List<AIBehaviour>();

    protected AIBehaviour ActiveBehaviour { get => activeBehaviour; }

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
        behaviour.Actiate();
        activeBehaviour = behaviour;
    }

    public virtual void OnCollisionEnter(Collision other){
        activeBehaviour?.OnCollisionEnter(other);
    }

    internal virtual bool IsInactive()
    {
        return false;
    }
}
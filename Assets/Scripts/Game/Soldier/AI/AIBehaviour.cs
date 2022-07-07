using System;
using UnityEngine;

public class AIBehaviour : MonoBehaviour
{
    private Soldier _soldier;
    protected Soldier soldier {
        get {
            if(!_soldier)
                _soldier = GetComponentInParent<Soldier>();
            return _soldier;
        }
    }

    protected bool active;
    public virtual void OnCollisionEnter(Collision other){ }
    public virtual void Activate(){ 
        var crrActivated = this.active;
        this.active = true;
        if(!crrActivated)
            OnActivated();
    }

    internal bool IsActivated()
    {
        return this.active;
    }

    public virtual void Deactivate(){
        var crrActivated = this.active; 
        this.active = false;
        if(crrActivated)
            OnDeactivated();
    }

    protected virtual void OnActivated(){}
    protected virtual void OnDeactivated(){}
}
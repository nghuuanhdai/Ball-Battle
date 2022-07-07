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
    public virtual void Actiate(){ 
        var crrActivated = this.active;
        this.active = true;
        if(!crrActivated)
            OnActivated();
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
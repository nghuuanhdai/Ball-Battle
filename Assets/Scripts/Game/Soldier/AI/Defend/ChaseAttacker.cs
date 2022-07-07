using UnityEngine;
using UnityEngine.Events;

public class ChaseAttacker : AIBehaviour {
    public UnityEvent OnAttackerCollide = new UnityEvent();
    [SerializeField] private FloatVariable chaseSpeed;
    public Soldier Target;
    
    private void Update() {
        if(!active) return;
        if(!Target) return;
        if(Target.HoldingBall == null)
            OnAttackerCollide.Invoke();
        soldier.Movement.Speed = chaseSpeed.Value;
        soldier.Movement.MoveTo(Target.transform);
    }

    public override void OnCollisionEnter(Collision other) {
        if(!this.active) return;
        var soldier = other.gameObject.GetComponent<Soldier>();
        if(!soldier) return;
        if(soldier == Target)
            OnAttackerCollide.Invoke();
    }

    protected override void OnDeactivated() {
        soldier.Movement.Speed = 0;
    }
}
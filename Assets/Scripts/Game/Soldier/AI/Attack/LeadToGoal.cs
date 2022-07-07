using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Events;

public class LeadToGoal : AIBehaviour {
    public UnityEvent OnDefenderCollide = new UnityEvent();
    [SerializeField] FloatVariable carryingSpeed;

    public Ball Ball;

    private void Update() {
        if(!this.active) return;

        soldier.Movement.Speed = carryingSpeed.Value;
        soldier.Movement.MoveTo(soldier.Team.OpponentGoal.transform);
        Ball?.Movement.TeleTo(soldier.transform.localPosition + soldier.transform.localRotation*Vector3.forward*0.5f);
    }

    protected override void OnDeactivated() {
        soldier.Movement.Speed = 0;
    }

    public override void OnCollisionEnter(Collision other) {
        if(!this.active) return;
        var soldier = other.gameObject.GetComponent<Soldier>();
        if(!soldier) return;
        if(soldier?.Team != this.soldier.Team)
        {
            OnDefenderCollide.Invoke();
        }
    }
}
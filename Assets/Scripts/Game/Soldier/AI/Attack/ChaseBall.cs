using UnityEngine;
using UnityEngine.Events;

public class ChaseBall : AIBehaviour {
    [SerializeField] FloatVariable chaseSpeed;

    private void Update() {
        if(!this.active) return;
        soldier.Movement.Speed = chaseSpeed.Value;
        if (soldier.Team.Ball.Holder != null && soldier.Team.Ball.Holder != soldier)
        {
            soldier.Movement.MoveInDirection(soldier.Team.OpponentTeamDirection);
        }
        else
        {
            soldier.Movement.MoveTo(soldier.Team.Ball.transform);
        }
    }

    protected override void OnDeactivated() {
        soldier.Movement.Speed = 0;
    }
}
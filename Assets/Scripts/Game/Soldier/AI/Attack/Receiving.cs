using UnityEngine;
using UnityEngine.Events;

public class Receiving : AIBehaviour {
    public UnityEvent OnReceiveComplete = new UnityEvent();
    [SerializeField] FloatVariable passingSpeed;
    public Ball Ball;

    protected override void OnActivated() {
        //TODO facing to ball
        Ball.Movement.Speed = passingSpeed.Value;
        Ball.Movement.MoveTo(soldier.transform);
    }

    protected override void OnDeactivated()
    {
        base.OnDeactivated();
        if(soldier.HoldingBall)
            soldier.HoldingBall.Movement.Speed = 0;
    }
}
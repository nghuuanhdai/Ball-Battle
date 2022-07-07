using UnityEngine;
using UnityEngine.Events;

public class Passing : AIBehaviour {
    public UnityEvent OnPassComplete = new UnityEvent();

    protected override void OnActivated() {
        var closest = soldier.Team.FindClosestSoldier(soldier);
        if(!closest)
        {
            soldier.Team.FailedToPass = true;
            return;
        }
        closest.ReceivePassing(soldier.HoldingBall);
        soldier.HoldingBall = null;
        OnPassComplete.Invoke();
    }
}
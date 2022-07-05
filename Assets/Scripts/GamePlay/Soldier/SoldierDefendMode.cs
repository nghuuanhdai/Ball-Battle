using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierDefendMode : SoldierBrain
{
    [SerializeField] private StandBy standBy;
    [SerializeField] private ChaseAttacker chaseAttacker;
    [SerializeField] private DefendInactive inactive;

    protected override SoldierBehaviour[] behaviours => new SoldierBehaviour[]{standBy, chaseAttacker, inactive};

    private void Awake() {
        /*
        standby.onDetectAttackerWithBall += switchState(chaseOpponent)
        chaseOpponent.onCaugth += switchState(inactive)
        inactive.onTimeOut += switchState(standby)
        */
        standBy.Vision.OnOpponentEnterVision.AddListener(opponent => {chaseAttacker.Opponent = opponent; SetBehaviour(chaseAttacker);});
        chaseAttacker.OnCatchAttacker += ()=> SetBehaviour(inactive);
    }

    private void Start() {
        SetBehaviour(standBy);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierDefendMode : SoldierBrain
{
    private void Awake() {
        /*
        standby.onDetectAttackerWithBall += switchState(chaseOpponent)
        chaseOpponent.onCaugth += switchState(inactive)
        inactive.onTimeOut += switchState(standby)
        */
    }

    private void Start() {
        //SetBehaviour(standby)
    }
}

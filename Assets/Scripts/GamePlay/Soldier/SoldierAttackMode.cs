using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierAttackMode : SoldierBrain
{
    private void Awake() {
        /*
        leadToGoal.onGetCaught += ()=> SetBehaviour(passing);
        passing.onComplete += () => SetBehaviour(inactive);
        passing.onFailed += ()=> { OnFailedToPass.Invoke(); SetBehaviour(inactive);}
        inactive.onTimeout += () => SetBehaviour(chaseBall)
        */
    }

    private void Start() {
        //SetBehaviour(chaseBall)
    }
}

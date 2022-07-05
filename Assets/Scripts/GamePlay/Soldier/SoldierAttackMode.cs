using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierAttackMode : SoldierBrain
{
    [SerializeField] private LeadToGoal leadToGoal;
    [SerializeField] private Passing passing;
    [SerializeField] private AttackInactive inactive;
    [SerializeField] private ChaseBall chaseBall;
    [SerializeField] private GoStraight goStraight;
    private Ball ball;
    private Soldier soldier;
    protected override SoldierBehaviour[] behaviours => new SoldierBehaviour[]{leadToGoal, passing, inactive, chaseBall, goStraight};
    
    private void Awake() {
        chaseBall.onGetBall += ball => {leadToGoal.Ball = ball; SetBehaviour(leadToGoal);};
        leadToGoal.OnBeingCaught += ()=> SetBehaviour(inactive);
        /*
        chaseBall.onGetBall += ()=> SetBehaviour(leadToGoal);
        leadToGoal.onGetCaught += ()=> SetBehaviour(passing);
        passing.onComplete += () => SetBehaviour(inactive);
        passing.onFailed += ()=> { OnFailedToPass.Invoke(); SetBehaviour(inactive);}
        inactive.onTimeout += () => SetBehaviour(chaseBall)
        */
    }

    private void Start() {
        soldier = GetComponentInParent<Soldier>();
        SetBehaviour(chaseBall);
    }

    private void Update() {
        if(ActiveBehaviour != inactive)
        {
            if(soldier.Ball.Soldier != null && soldier.Ball.Soldier != soldier && ActiveBehaviour != goStraight)
                SetBehaviour(goStraight);
        }
    }
}

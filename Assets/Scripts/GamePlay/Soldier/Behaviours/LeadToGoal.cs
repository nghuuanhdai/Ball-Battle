using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeadToGoal : SoldierBehaviour
{
    public event Action OnBeingCaught;
    [SerializeField] private FloatVariable carryingSpeed;
    private Soldier soldier;
    private SoldierMovement soldierMovement;

    public Ball Ball;

    private void Awake() {

        soldier = GetComponentInParent<Soldier>();
        soldierMovement = GetComponentInParent<SoldierMovement>();
    }

    private void Update() {
        soldierMovement.MoveSpeed = carryingSpeed.Value;
        soldierMovement.MoveTo(soldier.TargetGoal.transform.localPosition);
    }

    private void OnEnable() {
        Ball?.transform.SetParent(soldier.transform);
        Ball?.SetSoldier(soldier);
    }

    private void OnDisable() {
        soldierMovement.MoveSpeed = 0;
    }

    public override void OnCollisionEnter(Collision other)
    {
        var otherSoldier = other.gameObject.GetComponent<Soldier>();
        if(!otherSoldier || otherSoldier.TeamController == this.soldier.TeamController) return;
        OnBeingCaught?.Invoke();
    }
}

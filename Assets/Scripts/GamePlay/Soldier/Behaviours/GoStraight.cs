using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoStraight : SoldierBehaviour
{
    [SerializeField] private FloatVariable speed;
    private Soldier soldier;
    private SoldierMovement soldierMovement;

    private void Awake() {
        soldier = GetComponentInParent<Soldier>();
        soldierMovement = GetComponentInParent<SoldierMovement>();
    }

    private void Update() {
        soldierMovement.MoveSpeed = speed.Value;
        soldierMovement.MoveWithDirection(soldier.TargetFieldDirection);
    }

    private void OnDisable() {
        soldierMovement.MoveSpeed = 0;
    }
}

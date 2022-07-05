using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendInactive : SoldierBehaviour
{
    private Soldier soldier;
    private SoldierMovement soldierMovement;
    [SerializeField] private FloatVariable speed;
    private Vector3 originalPoint;

    private void Awake() {
        soldier = GetComponentInParent<Soldier>();
        soldierMovement = GetComponentInParent<SoldierMovement>();
        originalPoint = soldier.transform.localPosition;
    }

    private void Update() {
        soldierMovement.MoveSpeed = speed.Value;
        soldierMovement.MoveTo(originalPoint);
    }

    private void OnDisable() {
        soldierMovement.MoveSpeed = 0;
    }
}

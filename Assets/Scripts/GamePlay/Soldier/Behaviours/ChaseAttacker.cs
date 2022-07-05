using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAttacker : SoldierBehaviour
{
    public event Action OnCatchAttacker;
    [SerializeField] private FloatVariable speed;
    public Soldier Opponent;
    private Soldier soldier;
    private SoldierMovement soldierMovement;
    private void Awake() {
        soldier = GetComponentInParent<Soldier>();
        soldierMovement = GetComponentInParent<SoldierMovement>();
    }

    private void Update() {
        if(!Opponent) return;

        soldierMovement.MoveSpeed = speed.Value;
        soldierMovement.MoveTo(Opponent.transform.localPosition);
    }

    private void OnDisable() {
        soldierMovement.MoveSpeed = 0;
    }

    public override void OnCollisionEnter(Collision other)
    {
        var otherSoldier = other.gameObject.GetComponent<Soldier>();
        if(!otherSoldier || otherSoldier.TeamController == this.soldier.TeamController) return;
        OnCatchAttacker?.Invoke();
    }
}

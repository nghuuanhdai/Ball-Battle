using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseBall : SoldierBehaviour
{
    public event Action<Ball> onGetBall;
    [SerializeField] private FloatVariable chaseSpeed;
    private SoldierMovement soldierMovement;
    private Soldier soldier;
    private void Awake() {
        soldierMovement = GetComponentInParent<SoldierMovement>();
        soldier = GetComponentInParent<Soldier>();
    }

    private void Update() {
        soldierMovement.MoveSpeed = chaseSpeed.Value;
        soldierMovement.MoveTo(soldier?.Ball?.transform.localPosition??transform.position);
    }

    public override void OnCollisionEnter(Collision other) {
        if(!soldier?.Ball) return;
        var ball = other.gameObject.GetComponent<Ball>();
        if(ball != soldier?.Ball) return;
        if(ball.Soldier) return;
        onGetBall?.Invoke(ball);
    }

    private void OnDisable() {
        soldierMovement.MoveSpeed = 0;
    }
}

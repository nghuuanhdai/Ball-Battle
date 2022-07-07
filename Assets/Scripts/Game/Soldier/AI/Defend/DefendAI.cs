using System;
using System.Collections.Generic;
using UnityEngine;

public class DefendAI : AIController {
    [SerializeField] DefendInactive inactive;
    [SerializeField] Standby standby;
    [SerializeField] ChaseAttacker chaseAttacker;

    protected override List<AIBehaviour> behaviours => new List<AIBehaviour>(){ standby, chaseAttacker, inactive };

    private void Awake()
    {
        inactive.OnTimeout.AddListener(OnInactiveTimeOut);
        standby.OnDetectAttacker.AddListener(OnDetectAttacker);
        chaseAttacker.OnAttackerCollide.AddListener(OnAttackerCollide);
        inactive.OriginPosition = soldier.transform.localPosition;
    }

    private void OnDestroy() {
        inactive.OnTimeout.RemoveListener(OnInactiveTimeOut);
        standby.OnDetectAttacker.AddListener(OnDetectAttacker);
        chaseAttacker.OnAttackerCollide.RemoveListener(OnAttackerCollide);
    }

    private void OnDetectAttacker(Soldier attacker)
    {
        chaseAttacker.Target = attacker;
        SetBehaviour(chaseAttacker);
    }

    private void OnAttackerCollide()
    {
        SetBehaviour(inactive);
    }

    private void OnInactiveTimeOut()
    {
        SetBehaviour(standby);
    }

    internal override bool IsInactive()
    {
        return ActiveBehaviour == inactive;
    }
}
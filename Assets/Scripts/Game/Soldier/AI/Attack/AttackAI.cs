using System;
using System.Collections.Generic;
using UnityEngine;

public class AttackAI : AIController {
    [SerializeField] ChaseBall chaseBall;
    [SerializeField] LeadToGoal leadToGoal;
    [SerializeField] Passing passing;
    [SerializeField] Receiving receiving;
    [SerializeField] AttackInactive inactive;

    protected override List<AIBehaviour> behaviours => new List<AIBehaviour>(){ chaseBall, leadToGoal, passing, receiving, inactive };

    private void Awake()
    {
        leadToGoal.OnDefenderCollide.AddListener(OnDefenderCollide);
        passing.OnPassComplete.AddListener(OnPassingComplete);
        inactive.OnTimeout.AddListener(OnInactiveTimeout);
    }

    private void OnDestroy() {
        leadToGoal.OnDefenderCollide.RemoveListener(OnDefenderCollide);
        passing.OnPassComplete.RemoveListener(OnPassingComplete);
        inactive.OnTimeout.RemoveListener(OnInactiveTimeout);  
    }

    private void OnInactiveTimeout()
    {
        SetBehaviour(chaseBall);
    }

    private void OnPassingComplete()
    {
        Debug.Log("Passing Complete");
        SetBehaviour(inactive);
    }

    private void OnDefenderCollide()
    {
        SetBehaviour(passing);
    }

    public void ReceivePassing(Ball ball)
    {
        receiving.Ball = ball;
        SetBehaviour(receiving);

    }

    private void OnHitBall(Ball ball)
    {
        soldier.HoldingBall = ball;
        ball.Holder = soldier;
        leadToGoal.Ball = ball;
        SetBehaviour(leadToGoal);
    }

    public override void OnCollisionEnter(Collision other) {
        var ball = other.gameObject.GetComponent<Ball>();
        if(ball) OnHitBall(ball);
        ActiveBehaviour?.OnCollisionEnter(other);
        base.OnCollisionEnter(other);
    }

    internal override bool IsInactive()
    {
        return ActiveBehaviour == inactive;
    }
}
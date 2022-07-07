using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DefendInactive : AIBehaviour {
    public UnityEvent OnTimeout = new UnityEvent();
    [SerializeField] FloatVariable returnSpeed;
    [SerializeField] FloatVariable inactiveTime;
    [SerializeField] FloatVariable activeConsumption;
    public Vector3 OriginPosition;
    private Coroutine countDownCR;

    private void Update() {
        if(!active) return;
        soldier.Movement.Speed = returnSpeed.Value;
        soldier.Movement.MoveTo(OriginPosition);
    }

    protected override void OnDeactivated() {
        soldier.Movement.Speed = 0;
        soldier.Collider.enabled = true;
        if(countDownCR != null)
            StopCoroutine(countDownCR); 
    }

    protected override void OnActivated() {
        if(countDownCR != null)
            StopCoroutine(countDownCR); 
        countDownCR = StartCoroutine(ReactivateCountDownCR());
        soldier.Collider.enabled = false;
    }

    private IEnumerator ReactivateCountDownCR()
    {
        yield return new WaitForSeconds(inactiveTime.Value);
        yield return new WaitUntil(()=>soldier.Team.Energy.Value - activeConsumption.Value > 0);
        soldier.Team.Energy.Value -= activeConsumption.Value;
        OnTimeout.Invoke();
    }
}
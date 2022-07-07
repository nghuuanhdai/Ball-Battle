using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class AttackInactive : AIBehaviour {
    public UnityEvent OnTimeout = new UnityEvent();
    [SerializeField] FloatVariable inactiveTime;
    [SerializeField] FloatVariable activeConsumption;
    private Coroutine countDownCR;

    protected override void OnActivated() {
        //TODO turn to grayscale
        if(countDownCR != null)
            StopCoroutine(countDownCR); 
        countDownCR = StartCoroutine(ReactivateCountDownCR());
        soldier.Collider.enabled = false;
    }

    protected override void OnDeactivated()
    {
        soldier.Collider.enabled = true;  
        if(countDownCR != null)
            StopCoroutine(countDownCR);      
    }

    private IEnumerator ReactivateCountDownCR()
    {
        yield return new WaitForSeconds(inactiveTime.Value);
        yield return new WaitUntil(()=>soldier.Team.Energy.Value - activeConsumption.Value > 0);
        soldier.Team.Energy.Value -= activeConsumption.Value;
        OnTimeout.Invoke();
    }
}
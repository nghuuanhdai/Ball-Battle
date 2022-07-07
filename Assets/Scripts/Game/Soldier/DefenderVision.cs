using System;
using UnityEngine;
using UnityEngine.Events;

public class DefenderVision : MonoBehaviour {
    public UnityEvent<Soldier> OnOtherSoldierEnterFOV = new UnityEvent<Soldier>();
    public UnityEvent<Soldier> OnOtherSoldierExitFOV = new UnityEvent<Soldier>();

    public Func<Team> GetTeam;
    private void OnTriggerEnter(Collider other) {
        var soldier = other.gameObject.GetComponent<Soldier>();
        if(GetTeam?.Invoke() != soldier?.Team && soldier?.Team != null)
            OnOtherSoldierEnterFOV.Invoke(soldier);
    }

    private void OnTriggerExit(Collider other) {
        var soldier = other.gameObject.GetComponent<Soldier>();
        if(GetTeam?.Invoke() != soldier?.Team && soldier?.Team != null)
            OnOtherSoldierExitFOV.Invoke(soldier);
    }
}
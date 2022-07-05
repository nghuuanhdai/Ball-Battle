using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Vision : MonoBehaviour
{
    public UnityEvent<Soldier> OnOpponentEnterVision = new UnityEvent<Soldier>();
    private Soldier soldier;
    private List<Soldier> trackingSoldiers = new List<Soldier>();
    private void Awake() {
        soldier = GetComponentInParent<Soldier>();
    }

    private void Update() {
        foreach (var soldier in trackingSoldiers)
        {
            if(soldier.Ball.Soldier == soldier)
                OnOpponentEnterVision.Invoke(soldier);
        }
    }

    private void OnTriggerEnter(Collider other) {
        var soldier = other.GetComponent<Soldier>();
        if(!soldier) return;
        if(soldier.TeamController == this.soldier.TeamController)
            return;
        trackingSoldiers.Add(soldier);
    }

    private void OnTriggerExit(Collider other) {
        var soldier = other.GetComponent<Soldier>();
        if(!soldier) return;
        trackingSoldiers.Remove(soldier);
    }
}

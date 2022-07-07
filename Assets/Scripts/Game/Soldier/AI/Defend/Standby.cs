using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Standby : AIBehaviour {
    public UnityEvent<Soldier> OnDetectAttacker = new UnityEvent<Soldier>();
    [SerializeField] DefenderVision vision;
    private List<Soldier> enemiesInFOV = new List<Soldier>();

    private void Awake() {
        vision.GetTeam = ()=> soldier.Team;
        vision.OnOtherSoldierEnterFOV.AddListener(OnEnemyEnterFOV);
        vision.OnOtherSoldierExitFOV.AddListener(OnEnemyExitFOV);
    }

    private void OnDestroy() {
        vision.OnOtherSoldierEnterFOV.RemoveListener(OnEnemyEnterFOV);
        vision.OnOtherSoldierExitFOV.RemoveListener(OnEnemyExitFOV);
    }

    private void OnEnemyEnterFOV(Soldier enemy)
    {
        enemiesInFOV.Add(enemy);
    }

    private void OnEnemyExitFOV(Soldier enemy)
    {
        enemiesInFOV.Remove(enemy);
    }

    protected override void OnActivated()
    {
        vision.gameObject.SetActive(true);
        base.OnActivated();
    }

    protected override void OnDeactivated()
    {
        base.OnDeactivated();
        vision.gameObject.SetActive(false);
    }

    private void Update() {
        if(!active) return;

        foreach (var soldier in enemiesInFOV)
        {
            if(!soldier) continue;
            if(soldier.HoldingBall == soldier.Team.Ball)
            {
                OnDetectAttacker.Invoke(soldier);
                break;
            }   
        }
    }
}
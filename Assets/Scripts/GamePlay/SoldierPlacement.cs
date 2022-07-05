using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierPlacement : MonoBehaviour
{
    [SerializeField] private Soldier playerSoldierPrefab;
    [SerializeField] private Soldier opponentSoldierPrefab;
    [SerializeField] private Transform fieldRoot;
    [SerializeField] private UserTapInput input;
    [SerializeField] private FloatVariable playerEnergy, opponentEnergy;
    [SerializeField] private IntVariable spawnEnergyCost;
    [SerializeField] private Goal playerGoal, opponentGoal;
    [HideInInspector]
    public Ball Ball;
    public bool AllowOppoentTapSpawn = false;

    public TeamController PlayerTeam { get; internal set; }
    public TeamController OpponentTeam { get; internal set; }

    private void Awake() {
        input.OnTapPlayerSide.AddListener(OnTapPlayerSide);
        input.OnTapOpponentSide.AddListener(OnTapOpponentSide);
    }

    private void OnDestroy() {
        input.OnTapPlayerSide.RemoveListener(OnTapPlayerSide);
        input.OnTapOpponentSide.RemoveListener(OnTapOpponentSide);
    }

    private void OnTapOpponentSide(Vector3 worldPosition)
    {
        if(!AllowOppoentTapSpawn) return;
        if(opponentEnergy.Value - spawnEnergyCost.Value < 0) return;
        var newSoldier = Instantiate(opponentSoldierPrefab, worldPosition,Quaternion.identity,fieldRoot);
        newSoldier.Ball = Ball;
        newSoldier.TargetGoal = playerGoal;
        newSoldier.TeamController = OpponentTeam;
        newSoldier.TargetFieldDirection = playerGoal.transform.localPosition - opponentGoal.transform.localPosition ;
        newSoldier.GetComponent<SoldierBrainController>().PlayMode = OpponentTeam.PlayMode;
        opponentEnergy.Value -= spawnEnergyCost.Value;
    }

    private void OnTapPlayerSide(Vector3 worldPosition)
    {
        if(playerEnergy.Value - spawnEnergyCost.Value < 0) return;
        var newSoldier = Instantiate(playerSoldierPrefab, worldPosition,Quaternion.identity,fieldRoot);
        newSoldier.Ball = Ball;
        newSoldier.TargetGoal = opponentGoal;
        newSoldier.TeamController = PlayerTeam;
        newSoldier.TargetFieldDirection = opponentGoal.transform.localPosition - playerGoal.transform.localPosition;
        newSoldier.GetComponent<SoldierBrainController>().PlayMode = PlayerTeam.PlayMode;
        playerEnergy.Value -= spawnEnergyCost.Value;
    }
}

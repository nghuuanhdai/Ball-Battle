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
    public bool AllowOppoentTapSpawn = false;

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
        if(AllowOppoentTapSpawn)
            Instantiate(opponentSoldierPrefab, worldPosition,Quaternion.identity,fieldRoot);
    }

    private void OnTapPlayerSide(Vector3 worldPosition)
    {
        Instantiate(playerSoldierPrefab, worldPosition,Quaternion.identity,fieldRoot);
    }
}

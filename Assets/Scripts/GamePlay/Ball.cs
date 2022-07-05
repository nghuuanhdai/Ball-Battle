using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public event Action<Soldier> OnSoldierSet;
    public Soldier Soldier => soldier;
    private Soldier soldier;
    public bool IsInGoal;

    public void SetSoldier(Soldier soldier)
    {
        this.soldier = soldier;
        OnSoldierSet?.Invoke(soldier);
    }
}

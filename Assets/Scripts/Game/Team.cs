using System;
using System.Collections.Generic;
using UnityEngine;

public class Team
{
    public TeamPlayMode playMode;

    public Team(TeamPlayMode playMode)
    {
        this.playMode = playMode;
    }
    public TeamPlayMode PlayMode => playMode;

    public ColorVariable AccentColor { get; internal set; }

    public Ball Ball;
    public bool FailedToPass;
    public Vector3 OpponentTeamDirection;
    public Goal OpponentGoal;
    public List<Soldier> soldiers = new List<Soldier>();
    public FloatVariable Energy;

    internal Soldier FindClosestSoldier(Soldier soldier)
    {
        float closestDistance = Mathf.Infinity;
        Soldier closestSoldier = null;
        soldiers.RemoveAll(s => s == null);
        foreach (var sol in soldiers)
        {
            if(sol == soldier) continue;
            if(sol.IsInActive()) continue;
            var distance =( sol.transform.localPosition - soldier.transform.localPosition).magnitude;
            if(distance < closestDistance)
            {
                closestDistance = distance;
                closestSoldier = sol;
            }
        }
        return closestSoldier;
    }
}
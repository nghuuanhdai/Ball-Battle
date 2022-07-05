using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalColorReactor : ScriptableVariableReactor<Color>
{
    [SerializeField] private Goal goal;

    protected override void OnValueChanged()
    {
        if(goal)
            goal.Color = variable.Value;
    }
}

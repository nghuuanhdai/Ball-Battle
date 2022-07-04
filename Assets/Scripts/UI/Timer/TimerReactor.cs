using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerReactor : ScriptableVariableReactor<float>
{
    [SerializeField] private TimerUI timer;
    protected override void OnValueChanged()
    {
        timer.TimeLeft = Mathf.RoundToInt(variable.Value);
    }
}

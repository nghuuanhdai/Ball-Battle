using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxEnergyUIReactor : ScriptableVariableReactor<int>
{
    [SerializeField] private PlayerInfoUI playerInfoUI;
    protected override void OnValueChanged()
    {
        if(playerInfoUI)
            playerInfoUI.MaxEnergy = variable.Value;
    }
}

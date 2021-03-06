using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyUIReactor : ScriptableVariableReactor<float>
{
    [SerializeField] private PlayerInfoUI playerInfoUI;

    protected override void OnValueChanged()
    {
        if(playerInfoUI)
            playerInfoUI.Energy = variable.Value;
    }
}

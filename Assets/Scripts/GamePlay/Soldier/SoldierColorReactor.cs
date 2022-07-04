using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierColorReactor : ScriptableVariableReactor<Color>
{
    [SerializeField] private SoldierRenderer soldierRenderer;
    
    protected override void OnValueChanged()
    {
        if(soldierRenderer)
            soldierRenderer.Color = variable.Value;
    }
}

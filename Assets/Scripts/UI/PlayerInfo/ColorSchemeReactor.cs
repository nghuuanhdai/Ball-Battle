using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSchemeReactor : MonoBehaviour
{
    [SerializeField] private ColorVariable colorScheme;
    [SerializeField] private PlayerInfoUI playerInfoUI;
    private void Awake() {
        colorScheme.OnValueChanged.AddListener(OnColorChanged);
        OnColorChanged();
    }

    private void OnColorChanged()
    {
        if(playerInfoUI)
            playerInfoUI.AccentColor = colorScheme.Value;
    }

    private void OnDestroy() {
        colorScheme.OnValueChanged.RemoveListener(OnColorChanged);
    }
}

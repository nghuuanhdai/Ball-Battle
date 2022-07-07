using System;
using UnityEngine;

public class SoldierVisualizer : MonoBehaviour {
    private Team currentTeam;
    [SerializeField] SkinnedMeshRenderer skinnedMeshRenderer;

    public void SetTeam(Team team)
    {
        currentTeam?.AccentColor.OnValueChanged.RemoveListener(OnColorChanged);
        currentTeam = team;
        currentTeam?.AccentColor.OnValueChanged.AddListener(OnColorChanged);
        OnColorChanged();
    }

    private void OnDestroy() {
        currentTeam?.AccentColor.OnValueChanged.RemoveListener(OnColorChanged);
    }

    private void OnColorChanged()
    {
        skinnedMeshRenderer.material.color = currentTeam.AccentColor.Value;
    }
}
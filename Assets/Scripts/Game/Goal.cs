using System;
using UnityEngine;

public class Goal : MonoBehaviour {
    public Team Team;
    [SerializeField] Renderer _renderer;
    [SerializeField] ColorVariable teamColor;
    private void Awake() {
        teamColor.OnValueChanged.AddListener(OnColorChanged);
    }

    private void Start() {
        OnColorChanged();
    }

    private void OnDestroy() {
        teamColor.OnValueChanged.RemoveListener(OnColorChanged);
    }

    private void OnColorChanged()
    {
        _renderer.material.color = teamColor.Value;
    }
}
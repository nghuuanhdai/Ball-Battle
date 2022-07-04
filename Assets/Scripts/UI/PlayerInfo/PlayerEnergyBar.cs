using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEnergyBar : MonoBehaviour
{
    [SerializeField] private EnergyCell energyCellPrefab;
    [SerializeField] private Color _color;
    [SerializeField] private int _maxEnergy = 5;
    [SerializeField] private float _energy = 0;
    [SerializeField] private bool fillFromRight = false;

    public Color Color { get => _color; set {_color = value; OnColorChanged();} }
    public int MaxEnergy { get => _maxEnergy; set {_maxEnergy = value; OnMaxEnergyChanged();} }
    public float Energy { get => _energy; set {_energy = Mathf.Min(value, MaxEnergy); OnEnergyChanged();} }

    private void OnEnergyChanged()
    {
        var eCells = GetComponentsInChildren<EnergyCell>();
        for (int i = eCells.Length - 1; i >= 0; i--)
        {
            eCells[i].FillPercentage = Mathf.Clamp01(fillFromRight?(Energy+i+1-MaxEnergy):(Energy-i));
        }

    }

    private void OnMaxEnergyChanged()
    {
        var eCells = GetComponentsInChildren<EnergyCell>();
        if(eCells.Length == MaxEnergy)
            return;
        for (int i = 0; i < eCells.Length; i++)
            DestroyImmediate(eCells[i].gameObject);
        for (int i = 0; i < MaxEnergy; i++)
            Instantiate(energyCellPrefab, transform);
        OnEnergyChanged();
        OnColorChanged();
    }

    private void OnColorChanged()
    {
        var eCells = GetComponentsInChildren<EnergyCell>();
        foreach (var cell in eCells)
            cell.Color = Color;
    }
    
    private void OnValidate() {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.delayCall += () =>
        {
            if(Color != _color)
                Color = _color;
            if(MaxEnergy != _maxEnergy)
                MaxEnergy = _maxEnergy;
            if(Energy != _energy)
                Energy = _energy;
        }; 
        #endif
    }
}

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
    [SerializeField] private bool _fillFromRight = false;

    public Color Color { get => _color; set {_color = value; OnColorChanged();} }
    public int MaxEnergy { get => _maxEnergy; set {_maxEnergy = value; OnMaxEnergyChanged();} }
    public float Energy { get => _energy; set {_energy = Mathf.Min(value, MaxEnergy); OnEnergyChanged();} }
    public bool FillFromRight { get => _fillFromRight; set {_fillFromRight = value; OnFillFromRightChanged();}}

    private void OnEnergyChanged()
    {
        var eCells = GetComponentsInChildren<EnergyCell>();
        for (int i = eCells.Length - 1; i >= 0; i--)
        {
            eCells[i].FillPercentage = Mathf.Clamp01(FillFromRight?(Energy+i+1-MaxEnergy):(Energy-i));
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
        OnFillFromRightChanged();
    }

    private void OnFillFromRightChanged()
    {
        var eCells = GetComponentsInChildren<EnergyCell>();
        foreach (var cell in eCells)
            cell.FillFromRight = FillFromRight;
    }

    private void OnColorChanged()
    {
        var eCells = GetComponentsInChildren<EnergyCell>();
        foreach (var cell in eCells)
            cell.Color = Color;
    }
}

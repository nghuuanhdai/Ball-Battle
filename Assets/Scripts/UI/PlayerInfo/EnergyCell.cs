using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyCell : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private Color _color;
    [SerializeField] [Range(0, 1)] private float _fillPercentage;

    public Color Color { get => _color; set {_color = value; OnColorChanged();} }

    public float FillPercentage { get => _fillPercentage; set {_fillPercentage = value; OnFillPercentageChanged();} }

    private void OnColorChanged()
    {
        if(fillImage)
            fillImage.color = Color;
    }

    private void OnFillPercentageChanged()
    {
        if(fillImage)
            fillImage.fillAmount = FillPercentage;
    }

    private void OnValidate() {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.delayCall += () =>
        {
            Color = _color;
            FillPercentage = _fillPercentage;
        };
        #endif
    }
}

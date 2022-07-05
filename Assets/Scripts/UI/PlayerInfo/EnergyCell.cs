using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyCell : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private Color _color;
    [SerializeField] [Range(0, 1)] private float _fillPercentage;
    [SerializeField] private bool _fillFromRight;

    public Color Color { get => _color; set {_color = value; OnColorChanged();} }

    public float FillPercentage { get => _fillPercentage; set {_fillPercentage = value; OnFillPercentageChanged();} }
    public bool FillFromRight { get => _fillFromRight; set {_fillFromRight = value; OnFillFromRightChanged();}}

    private void OnFillFromRightChanged()
    {
        fillImage.fillOrigin = FillFromRight?1:0;
    }
    
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
}

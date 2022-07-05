using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierRenderer : MonoBehaviour
{
    [SerializeField] private Material soldierMaterial;
    [SerializeField] private Color _color;
    public Color Color {get => _color; set {_color = value; OnColorChanged();}}

    private void OnColorChanged()
    {
        if(soldierMaterial)
            soldierMaterial.color = Color;
    }
}

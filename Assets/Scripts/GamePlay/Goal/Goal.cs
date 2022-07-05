using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private Material goalMaterial;
    [SerializeField] private Color _color;
    public Color Color {get => _color; set { _color = value; OnColorChanged(); }}

    private void OnColorChanged()
    {
        if(goalMaterial)
            goalMaterial.color = Color;
    }
}

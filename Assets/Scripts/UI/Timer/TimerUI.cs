using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private TMP_Text timeLeftText;
    [SerializeField] private Image header;
    [SerializeField] private Color _color;
    [SerializeField] private int _timeLeft;

    public Color Color { get => _color; set {_color = value; OnColorChanged();} }
    public int TimeLeft { get => _timeLeft; set {_timeLeft = Mathf.Max(value, 0); OnTimeLeftChanged(); }}

    private void OnColorChanged()
    {
        if(header)
            header.color = Color;
        if(timeLeftText)
            timeLeftText.color = Color;
    }

    private void OnTimeLeftChanged()
    {
        if(timeLeftText)
            timeLeftText.text = $"{TimeLeft}s";
    }

    private void OnValidate() {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.delayCall += () =>
        {
            Color = _color;
            TimeLeft = _timeLeft;
        }; 
        #endif
    }
}

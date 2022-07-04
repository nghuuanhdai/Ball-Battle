using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class PlayerInfoText : MonoBehaviour
{
    [SerializeField] private TMP_Text textUI;
    [SerializeField] private Color _textColor;
    [SerializeField] private string _text;

    public Color TextColor { get => _textColor; set {_textColor = value; OnTextColorChanged();} }
    public string Text { get => _text; set {_text = value; OnTextChanged();} }

    private void OnTextChanged()
    {
        if(textUI)
            textUI.text = Text;
    }

    private void OnTextColorChanged()
    {
        if(textUI)
            textUI.color = TextColor;
    }

    private void OnValidate() {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.delayCall += () =>
        {
            TextColor = _textColor;
            Text = _text;
        };
        #endif
    }
}

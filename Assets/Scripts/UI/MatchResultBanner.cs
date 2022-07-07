using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MatchResultBanner : MonoBehaviour
{
    private string _bannerText;
    public string BannerText {
        get => _bannerText;
        set {
            _bannerText = value;
            OnTextChanged();
        }
    }
    [SerializeField] TMP_Text text;
    private void OnTextChanged()
    {
        text.text = BannerText;
    }
}

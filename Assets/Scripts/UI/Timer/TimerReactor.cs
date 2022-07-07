using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerReactor : MonoBehaviour
{
    [SerializeField] private FloatVariable MatchMaxTime;
    [SerializeField] private FloatVariable MatchTime;

    [SerializeField] private TimerUI timer;
    private void Awake() {
        MatchMaxTime.OnValueChanged.AddListener(OnTimeLeftChanged);
        MatchTime.OnValueChanged.AddListener(OnTimeLeftChanged);
    }

    private void OnDestroy() {
        MatchMaxTime.OnValueChanged.RemoveListener(OnTimeLeftChanged);
        MatchTime.OnValueChanged.RemoveListener(OnTimeLeftChanged);
    }

    private void OnTimeLeftChanged()
    {
        timer.TimeLeft = Mathf.RoundToInt(Mathf.Max(0, MatchMaxTime.Value - MatchTime.Value));
    }
}

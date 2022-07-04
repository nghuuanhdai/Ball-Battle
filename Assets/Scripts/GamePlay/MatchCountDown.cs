using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchCountDown : MonoBehaviour
{
    [SerializeField] private FloatVariable timeLeftVariable;
    [SerializeField] private FloatVariable matchTime;

    public float MatchStartTime;
    private void Update() {
        timeLeftVariable.Value = Mathf.Max(0, matchTime.Value - (Time.time - MatchStartTime));
    }
}

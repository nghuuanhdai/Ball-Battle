using UnityEngine;

public class Timer : MonoBehaviour {
    [SerializeField] FloatVariable maxMatchTime;
    [SerializeField] FloatVariable matchTime;

    private float startTime;
    public void Reset(){
        startTime = Time.time;
    }
    public bool TimeOut
    {
        get => matchTime.Value >= maxMatchTime.Value;
    }

    private void Update() {
        matchTime.Value = Time.time - startTime;
    }
}
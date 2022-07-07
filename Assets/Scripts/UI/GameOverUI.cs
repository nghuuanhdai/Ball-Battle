using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour {
    public UnityEvent OnReplay = new UnityEvent();
    public UnityEvent OnReturn = new UnityEvent();

    public GameResultData ScoreData;

    [SerializeField] Button replayBtn, returnBtn;

    private void Awake() {
        replayBtn.onClick.AddListener(OnReplay.Invoke);
        returnBtn.onClick.AddListener(OnReturn.Invoke);
    }

    private void OnDestroy() {
        replayBtn.onClick.RemoveListener(OnReplay.Invoke);
        returnBtn.onClick.RemoveListener(OnReturn.Invoke);   
    }
}
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour {
    public UnityEvent OnSinglePlayerMode = new UnityEvent();
    public UnityEvent OnMultiplayerMode = new UnityEvent();
    public UnityEvent OnAR = new UnityEvent();

    [SerializeField] Button singleModeBtn, multiplayerModeBtn, arBtn;

    private void Awake() {
        singleModeBtn.onClick.AddListener(OnSinglePlayerMode.Invoke);
        multiplayerModeBtn.onClick.AddListener(OnMultiplayerMode.Invoke);
        arBtn.onClick.AddListener(OnAR.Invoke);
    }

    private void OnDestroy() {
        singleModeBtn.onClick.RemoveListener(OnSinglePlayerMode.Invoke);
        multiplayerModeBtn.onClick.RemoveListener(OnMultiplayerMode.Invoke);
        arBtn.onClick.RemoveListener(OnAR.Invoke);
    }
}
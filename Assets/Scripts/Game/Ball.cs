using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Animations;

public class Ball : MonoBehaviour {
    public UnityEvent OnHolderChanged = new UnityEvent();
    
    [SerializeField] MovementController _movement;    
    public MovementController Movement => _movement;

    private Soldier _holder;
    public Soldier Holder {
        get => _holder;
        set {
            _holder = value;
            OnHolderChanged.Invoke();
        }
    }

    public Goal Goal;
}
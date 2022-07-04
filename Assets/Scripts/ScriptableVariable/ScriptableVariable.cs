using UnityEngine;
using UnityEngine.Events;

public class ScriptableVariable<T> : ScriptableObject where T:struct {
    public UnityEvent OnValueChanged = new UnityEvent();
    [SerializeField]
    private T _value;
    public T Value {get => _value; set {_value = value; OnValueChanged.Invoke();}}
}
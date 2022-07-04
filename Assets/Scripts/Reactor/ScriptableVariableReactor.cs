using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptableVariableReactor<T> : MonoBehaviour where T:struct
{
    [SerializeField] protected ScriptableVariable<T> variable;
    private void Awake() {
        variable.OnValueChanged.AddListener(OnValueChanged);
        OnValueChanged();
    }

    private void OnDestroy() {
        variable.OnValueChanged.RemoveListener(OnValueChanged);
    }

    protected abstract void OnValueChanged();
}

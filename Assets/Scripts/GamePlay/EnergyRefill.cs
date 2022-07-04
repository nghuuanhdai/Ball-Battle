using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyRefill : MonoBehaviour
{
    [SerializeField] private IntVariable maxEnergy;
    [SerializeField] private FloatVariable energy;
    [SerializeField] private FloatVariable refillRate;

    private void Awake() {
        energy.Value = 0;
    }

    private void Update() {
        energy.Value = Mathf.Min(maxEnergy.Value, energy.Value + refillRate.Value*Time.deltaTime);
    }
}

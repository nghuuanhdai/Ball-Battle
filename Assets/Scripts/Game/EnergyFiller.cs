using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyFiller : MonoBehaviour
{
    [SerializeField] private FloatVariable energy;
    [SerializeField] private IntVariable maxEnergy;
    [SerializeField] private FloatVariable fillRate;

    public void Reset()
    {
        energy.Value = maxEnergy.Value;
    }

    private void Update() {
        energy.Value = Mathf.Clamp(energy.Value + fillRate.Value*Time.deltaTime,0,maxEnergy.Value);
    }
}

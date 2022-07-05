using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierBrainController : MonoBehaviour
{   
    [SerializeField] private SoldierBrain attackMode, defendMode;
    [SerializeField] private PlayMode _playMode;
    public PlayMode PlayMode { get => _playMode; set { _playMode = value; SwitchMode(_playMode); }}

    private void Awake() {
        SwitchMode(PlayMode);
    }

    private void SwitchMode(PlayMode playMode)
    {
        switch (playMode)
        {
            case PlayMode.Attacker:
                SetBrain(attackMode);
                break;
            case PlayMode.Defender:
                SetBrain(defendMode);
                break;
        }
    }

    private void SetBrain(SoldierBrain brain)
    {
        attackMode.gameObject.SetActive(brain == attackMode);
        defendMode.gameObject.SetActive(brain == defendMode);
    }

    private void OnCollisionEnter(Collision other) {
        GetActiveBehaviour()?.OnCollisionEnter(other);
    }

    private void OnTriggerEnter(Collider other) {
        GetActiveBehaviour()?.OnTriggerEnter(other);
    }

    private SoldierBehaviour GetActiveBehaviour()
    {
        var activeBrain = GetActiveBrain();
        return activeBrain?.ActiveBehaviour;
    }

    private SoldierBrain GetActiveBrain()
    {
        if(attackMode.gameObject.activeSelf)
            return attackMode;
        if(defendMode.gameObject.activeSelf)
            return defendMode;
        return null;
    }
}

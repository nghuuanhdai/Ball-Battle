using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierBrainController : MonoBehaviour
{   
    [SerializeField] private SoldierBrain attackMode, defendMode;

    public void SwitchMode(PlayMode playMode)
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
        //activeBehaviour.OnCollisionEnter(other)
    }

    private void OnTriggerEnter(Collider other) {
        //activeBehaviour.OnTriggerEnter(other)
    }
}

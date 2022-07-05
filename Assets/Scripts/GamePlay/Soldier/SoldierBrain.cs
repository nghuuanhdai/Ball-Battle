using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierBrain : MonoBehaviour
{
    public SoldierBehaviour ActiveBehaviour {get; protected set;}
    protected virtual SoldierBehaviour[] behaviours => new SoldierBehaviour[]{};

    protected void SetBehaviour(SoldierBehaviour behaviour)
    {
        foreach (var bh in behaviours)
            bh.gameObject.SetActive(false);
        behaviour.gameObject.SetActive(true);
        ActiveBehaviour = behaviour;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierBehaviour : MonoBehaviour
{
    public virtual void OnCollisionEnter(Collision other){}
    public virtual void OnTriggerEnter(Collider other){}
}

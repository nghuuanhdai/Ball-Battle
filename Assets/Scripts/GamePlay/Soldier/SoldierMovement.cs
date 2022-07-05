using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierMovement : MonoBehaviour
{
    public float MoveSpeed;
    private Vector3 direction;

    public void MoveTo(Vector3 fieldLocalPosition)
    {
        this.direction = (fieldLocalPosition - transform.localPosition).normalized;
    }

    private void FixedUpdate() {
        transform.localPosition += direction*MoveSpeed*Time.fixedDeltaTime;
    }

    public void MoveWithDirection(Vector3 targetFieldDirection)
    {
        this.direction = targetFieldDirection.normalized;
    }
}

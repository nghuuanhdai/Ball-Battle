using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovementController : MonoBehaviour
{
    private Rigidbody rb;
    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    private Vector3 direction;
    public float Speed = 1;

    internal void MoveInDirection(Vector3 direction)
    {
        this.direction = direction.normalized;
    }

    internal void MoveTo(Vector3 fieldPosition)
    {
        var dir = (fieldPosition - transform.localPosition);
        this.direction = dir.normalized;
        if(dir.magnitude < 0.1f)
            this.Speed = 0;
    }

    internal void TeleTo(Vector3 fieldPosition)
    {
        var worldTargetPosition = transform.parent.TransformPoint(fieldPosition);
        rb.position = worldTargetPosition;
        transform.position = rb.position;
    }

    internal void MoveTo(Transform target)
    {
        var dir = (target.localPosition - transform.localPosition);
        this.direction = dir.normalized;
        if(dir.magnitude < 0.1f)
            this.Speed = 0;
    }

    private void FixedUpdate() {
        var targetPosition = transform.localPosition + direction*Speed*Time.fixedDeltaTime;
        var worldTargetPosition = transform.parent.TransformPoint(targetPosition);
        var lookDir = worldTargetPosition - rb.position;
        rb.position = worldTargetPosition;
        if(lookDir.magnitude > 0)
            rb.rotation = Quaternion.LookRotation(lookDir);
    }
}
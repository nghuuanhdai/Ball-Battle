using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UserTapInput : MonoBehaviour
{
    public UnityEvent<Vector3> OnTapPlayerSide, OnTapOpponentSide;
}

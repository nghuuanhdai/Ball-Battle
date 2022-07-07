using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TouchDetector : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent<Vector3> OnTouched = new UnityEvent<Vector3>();
    [SerializeField] private Transform fieldContainer;

    public void OnPointerClick(PointerEventData eventData)
    {
        OnTouched.Invoke(fieldContainer.InverseTransformPoint(eventData.pointerCurrentRaycast.worldPosition));
    }
}

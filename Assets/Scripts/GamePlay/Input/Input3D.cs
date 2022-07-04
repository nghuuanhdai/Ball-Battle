using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input3D : UserTapInput
{
    [SerializeField] private Camera mCamera;
    [SerializeField] private Collider playerSideCollider, opponentSideCollider;
    private void Update() {
        if(Input.GetMouseButtonDown(0))
        {
            if(playerSideCollider.Raycast(mCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit pHit, mCamera.farClipPlane))
            {
                OnTapPlayerSide.Invoke(pHit.point);
            }
            if(opponentSideCollider.Raycast(mCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit oHit, mCamera.farClipPlane))
            {
                OnTapOpponentSide.Invoke(oHit.point);
            }
        }
    }
}

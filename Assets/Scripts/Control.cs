using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.InputSystem;

public class Control : MonoBehaviour
{
    public Vector3 FocalPoint;
    public Vector2 Offset;
    Vector2 _mouse;
    Vector3 _lookPos;

    void FixedUpdate()
    {
        _mouse = Mouse.current.position.ReadValue();

        if (State.OnTrain)
            transform.eulerAngles = new Vector3(
                (((_mouse.y / Screen.height) - 0.5f) * 5) + Offset.x,
                (((_mouse.x / Screen.width) - 0.5f) * -5) + Offset.y,
                transform.eulerAngles.z
            );
        else
        {
            transform.LookAt(Vector3.Lerp(State.PlayersMidpoint, FocalPoint, 0.75f));
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(FocalPoint, Vector3.one);
    }
#endif
}

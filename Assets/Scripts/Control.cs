using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.InputSystem;

public class Control : MonoBehaviour
{
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
            _lookPos = State.Player.transform.position;

            transform.LookAt(_lookPos);

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float Acceleration;
    private Rigidbody rb;

    private void Awake()
    {
        State.Player = gameObject;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(
            new Vector3(
                (Keyboard.current.dKey.ReadValue() - Keyboard.current.aKey.ReadValue()) * Acceleration,
                0,
                (Keyboard.current.wKey.ReadValue() - Keyboard.current.sKey.ReadValue()) * Acceleration
            ), 
            ForceMode.Impulse
        );

        if (Keyboard.current.eKey.wasPressedThisFrame) State.OnTrain = !State.OnTrain;
    }
}

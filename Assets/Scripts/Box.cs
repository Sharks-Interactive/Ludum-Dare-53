using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public SItem repo;
    private Player _player;

    private void Update()
    {
        if (_player != null)
        {
            if (Input.GetActionInput(_player.ID).wasPressedThisFrame)
            {
                _player.HeldItem = repo;
                Input.Feedback(_player.ID, new Vector2(1, 0), 250);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        _player = other.GetComponent<Player>();
    }

    private void OnTriggerExit(Collider other)
    {
        _player = null;
    }
}

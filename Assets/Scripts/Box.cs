using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public SItem repo;

    private void OnTriggerStay(Collider other)
    {
        Player _player = other.GetComponent<Player>();
        if (_player != null)
        {
            if (Input.GetActionInput(_player.ID).wasPressedThisFrame)
            {
                _player.HeldItem = repo;
                Input.Feedback(_player.ID, new Vector2(1, 0), 250);
            }
        }
    }
}

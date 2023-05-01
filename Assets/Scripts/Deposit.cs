using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deposit : MonoBehaviour
{
    public SItem HeldItem
    {
        get => _heldItem;
        set
        {
            _heldItem = value;
            foreach (GameObject item in Item)
            {
                item.SetActive(false);
            }
            if (_heldItem == null) return;
            Item[_heldItem.Type - 1].SetActive(true);
        }
    }
    private SItem _heldItem;

    public GameObject[] Item;

    private void OnTriggerStay(Collider other)
    {
        Player _player = other.GetComponent<Player>();
        if (_player != null)
        {
            if (Input.GetActionInput(_player.ID).isPressed)
            {
                if (_player.HeldItem != null && HeldItem == null)
                {
                    HeldItem = _player.HeldItem;
                    _player.HeldItem = null;

                    Input.Feedback(_player.ID, new Vector2(1, 0), 250);
                    return;
                }
                if (_player.HeldItem == null && HeldItem != null)
                {
                    _player.HeldItem = HeldItem;
                    HeldItem = null;

                    Input.Feedback(_player.ID, new Vector2(1, 0), 250);
                    return;
                }
            }
        }
    }
}

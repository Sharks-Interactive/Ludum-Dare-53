using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Packager : MonoBehaviour
{
    public SItem HeldItem
    {
        get => _heldItem;
        set
        {
            _heldItem = value;
        }
    }
    private SItem _heldItem;
    private Player _player;

    public float TotalProgress;
    public float Progress = 0.1f;
    private float _progress;

    private Image _progressImage;

    public SItem i1;
    public SItem i2;

    public GameObject[] Item;

    private void Awake()
    {
        _progressImage = transform.Find("Canvas").Find("Fill").GetComponent<Image>();
    }

    private void Update()
    {
        if (_player != null)
        {
            if (Input.GetActionInput(_player.ID).isPressed)
            {
                if (_player.HeldItem != null && HeldItem == null)
                    if (_player.HeldItem.Type == 3 || _player.HeldItem.Type == 0)
                        return;
                if (_player.HeldItem != null)
                    HeldItem = _player.HeldItem;
                if (_player.HeldItem == null && HeldItem == null) return;
                _player.HeldItem = null;

                _progress += Progress * Time.deltaTime;
                _progressImage.fillAmount = _progress / TotalProgress;

                if (_progress > TotalProgress)
                {
                    if (HeldItem != null)
                        _player.HeldItem = HeldItem.Type == 1 ? i1 : i2;
                    HeldItem = null;
                    _progress = 0;
                    _progressImage.fillAmount = _progress / TotalProgress;
                }
            }
            else
            {
                _progress = 0;
                _progressImage.fillAmount = _progress / TotalProgress;
                if (HeldItem != null)
                    _player.HeldItem = HeldItem;
                HeldItem = null;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        _player = other.GetComponent<Player>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (HeldItem != null)
            _player.HeldItem = HeldItem;
        HeldItem = null;
        _player = null;

        _progress = 0;
        _progressImage.fillAmount = _progress / TotalProgress;
    }
}

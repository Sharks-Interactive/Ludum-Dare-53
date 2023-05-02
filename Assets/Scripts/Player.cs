using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float Acceleration;
    public int ID;

    public SItem HeldItem
    {
        get => _heldItem;
        set { 
            _heldItem = value;
            _itemDisplay.fillAmount = value == null ? 0 : 1;
            if (value != null)
                _itemDisplay.sprite = value.Icon;
        }
    }

    private Rigidbody rb;
    private SItem _heldItem;

    private Image _itemDisplay;

    private void Awake()
    {
        _itemDisplay = transform.Find("Canvas").GetComponentInChildren<Image>();
        State.Players[ID] = gameObject;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if (ID > Input.ConnectedPlayers - 1) return;
        rb.AddForce(
            new Vector3(
                Input.ReadAxis(ID, 0) * Acceleration,
                0,
                (Input.ReadAxis(ID, 1) * Acceleration) + (Mathf.Abs(transform.position.x) < 4 && transform.position.z < 2.5 ? 0 : (State.TrainSpeed / 4))
            ), 
            ForceMode.Impulse
        );

        if (transform.position.z < -10 || transform.position.y < -2)
        {
            transform.position = new Vector3(6 * (ID == 0 ? -1 : 1), 2, 0);
            HeldItem = null;
            Input.Feedback(ID, new Vector2(0, 1), 500);
        }
    }

    void Update()
    {
        if (ID > Input.ConnectedPlayers - 1) return;
        //if (Input.GetActionInput(ID).wasPressedThisFrame) State.OnTrain = !State.OnTrain;
    }

    public void AddResources(SItem item)
    {
        Input.Feedback(ID, new Vector2(1, 0), 250);
        HeldItem = item;
    }

    private void OnTriggerEnter(Collider collision)
    {
        collision.BroadcastMessage("UpdateHarvestState", true, SendMessageOptions.DontRequireReceiver);
        collision.BroadcastMessage("UpdateReceiver", this, SendMessageOptions.DontRequireReceiver);
    }

    private void OnTriggerExit(Collider collision)
    {
        collision.BroadcastMessage("UpdateHarvestState", false, SendMessageOptions.DontRequireReceiver);
    }
}

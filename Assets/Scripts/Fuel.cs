using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Fuel : MonoBehaviour
{
    private float _harvestProgress = 1;
    private bool _harvesting = false;
    private Vector3 _startSize;
    private Player _receiver;

    public SItem Resource;
    public float HarvestSpeed = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        _startSize = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (_receiver != null)
            _harvesting = Input.GetActionInput(_receiver.ID).isPressed;
        else _harvesting = false;

        _harvestProgress += HarvestSpeed * Time.deltaTime * (_harvesting ? -2.5f : 1);
        _harvestProgress = Mathf.Clamp01(_harvestProgress);

        transform.localScale = Vector3.Lerp(Vector3.zero, _startSize, _harvestProgress);
        if (_harvestProgress < 0.35f)
        {
            _receiver.AddResources(Resource);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        _receiver = other.GetComponent<Player>();
    }

    private void OnTriggerExit(Collider other)
    {
        _receiver = null;
    }
}

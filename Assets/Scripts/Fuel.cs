using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        _harvestProgress += HarvestSpeed * Time.deltaTime * (_harvesting ? -2.5f : 1);
        _harvestProgress = Mathf.Clamp01(_harvestProgress);

        transform.localScale = Vector3.Lerp(Vector3.zero, _startSize, _harvestProgress);
        if (_harvestProgress < 0.1f)
        {
            _receiver.AddResources(Resource);
            gameObject.SetActive(false);
        }
    }

    public void UpdateHarvestState(bool state) => _harvesting = state;
    public void UpdateReceiver(Player receiver) => _receiver = receiver;
}

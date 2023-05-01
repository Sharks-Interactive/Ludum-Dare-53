using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public Vector3 PlayerOneWarp;
    public Vector3 PlayerTwoWarp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Player _player = other.GetComponent<Player>();
        if (_player != null)
        {
            _player.transform.position = _player.ID == 0 ? PlayerOneWarp : PlayerTwoWarp;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(PlayerOneWarp, Vector3.one);
        Gizmos.DrawCube(PlayerTwoWarp, Vector3.one);
    }
#endif
}

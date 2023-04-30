using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void StateChange(bool state);

public static class State
{
    public static bool OnTrain 
    {
        get => _onTrain;
        set
        {
            _onTrain = value;
            if (OnPlayerStateChanged != null)
                OnPlayerStateChanged(value);
        }
    }
    private static bool _onTrain = true;

    public static StateChange OnPlayerStateChanged;

    public static GameObject Player;

    public static Vector3 Divide (this Vector3 a, Vector2 b)
    {
        return new Vector3(a.x / b.x, a.y / b.y, a.z);
    }
}

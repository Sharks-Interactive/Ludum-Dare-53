using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public delegate void StateChange(bool state);

public static class State
{
    public static float TrainSpeed = -3.5f;

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
    private static bool _onTrain = false;

    public static StateChange OnPlayerStateChanged;

    public static Vector3 PlayersMidpoint
    {
        get => Vector3.Lerp(Players[0].transform.position, Players[1].transform.position, 0.5f);
    }
    public static GameObject[] Players = new GameObject[2];

    public static Vector3 Divide (this Vector3 a, Vector2 b)
    {
        return new Vector3(a.x / b.x, a.y / b.y, a.z);
    }

    public static void EndGame(string reason)
    {
        GameObject.Find("EndPanel").GetComponent<CanvasGroup>().alpha = 1.0f;
        GameObject.Find("exp").GetComponent<TextMeshProUGUI>().text = reason;
        Time.timeScale = 0.0f;
    }
}

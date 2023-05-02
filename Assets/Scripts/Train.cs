using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Train : MonoBehaviour
{
    public int MaxFuel = 50;
    public float Fuel {
        get => _fuel;
        set { 
            _fuel = value;
            fill.fillAmount = value / MaxFuel;

            _showBar = fill.fillAmount < 0.5;

            ParticleSystem.EmissionModule emission = smoke.emission;
            emission.rateOverTime = fill.fillAmount * 3;

            State.TrainSpeed = fill.fillAmount * -3.5f;
        }
    }

    public float FuelDrain = 0.05f;

    private bool _showBar = true;
    private float _fuel;

    private CanvasGroup cv;
    private Image fill;

    private ParticleSystem smoke;

    // Start is called before the first frame update
    void Start()
    {
        cv = GetComponentInChildren<CanvasGroup>();
        fill = transform.Find("Canvas").Find("Fill").GetComponent<Image>();

        smoke = GetComponentInChildren<ParticleSystem>();
        Fuel = MaxFuel;
    }

    // Update is called once per frame
    void Update()
    {
        cv.alpha += 0.8f * Time.deltaTime * (_showBar ? 1 : -1);
        Fuel -= FuelDrain * Time.deltaTime;

        if (Fuel < 0) State.EndGame("the train ran out of steam.");
    }

    private void OnTriggerEnter(Collider other)
    {
        Player _collision = other.GetComponent<Player>();
        if (_collision == null) return;
        if (_collision.HeldItem == null) return;

        if (_collision.HeldItem.Type == 0)
        {
            Fuel += _collision.HeldItem.Value;
            _collision.HeldItem = null;
            Input.Feedback(_collision.ID, new Vector2(0.1f, 0.1f), 100);
        }
    }
}

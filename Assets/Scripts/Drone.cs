using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Drone : MonoBehaviour
{
    public float Speed = 5;
    public SItem s1, s2;

    public SItem mine;
    private Image Item;
    private Image Fill;

    private float _progress = 0;
    private bool _dead = false;

    public float TimerTotal = 5;
    private float _timer = 0;

    public Vector3 start;

    private float offset;

    // Start is called before the first frame update
    void Start()
    {
        Item = transform.Find("Canvas").Find("Fill").GetComponent<Image>();
        Fill = transform.Find("Canvas").Find("Progress").GetComponent<Image>();
        StartNew();

        start = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        _progress += (!_dead ? -0.1f : 0.1f) * Time.deltaTime;
        _progress = Mathf.Clamp01(_progress);
        if (!_dead) _timer -= 0.1f * Time.deltaTime;
        Fill.fillAmount = _timer / TimerTotal;

        transform.position = Vector3.Lerp(Vector3.Lerp(
                new Vector3(start.x - 0.005f, start.y, start.z - 0.005f),
                new Vector3(start.x + 0.01f, start.y + offset, start.z + 0.01f),
                (1 + Mathf.Sin(Time.time + (offset * 13))) / 2
            ),
            new Vector3(transform.position.x, 35, transform.position.z),
            _progress
        );

        if (_timer < 0) State.EndGame("a drone didn't get it's package in time.");
    }

    async void StartNew()
    {
        _dead = true;
        await System.Threading.Tasks.Task.Delay(Random.Range(3000, 18000));

        // im so bored
        if (Random.Range(0, 2) == 0) mine = s1; else mine = s2;
        Item.sprite = mine.Icon;

        _timer = TimerTotal;
        Fill.fillAmount = _timer / TimerTotal;

        offset = Random.Range(0.3f, 3);

        _dead = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Player _player = other.GetComponent<Player>();
        if (_player != null)
        {
            if (_player.HeldItem != null)
                if (_player.HeldItem.Type == 3)
                {
                    if (mine == s1 && _player.HeldItem.Name == "ComputerBox")
                    {
                        _player.HeldItem = null;
                        StartNew();
                    }

                    if (mine == s2 && _player.HeldItem.Name == "MouseBox")
                    {
                        _player.HeldItem = null;
                        StartNew(); 
                    }
                }
        }
    }
}

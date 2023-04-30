using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneryController : MonoBehaviour
{
    public Transform[] SceneryBlocks = new Transform[20];
    private Transform SceneryProps;
    private void Start()
    {
        SceneryProps = transform.Find("Props");
    }

    void Update()
    {
        MoveScenery();
        MoveProps();
    }

    void MoveScenery()
    {
        foreach (Transform block in SceneryBlocks)
        {
            block.position += new Vector3(0, 0, State.TrainSpeed * Time.deltaTime);
            if (block.position.z < -52) block.position = new Vector3(0, 0, ((SceneryBlocks.Length - 1) * 52) - (-52 - block.position.z));
        }
    }

    void MoveProps()
    {
        foreach (Transform prop in SceneryProps)
        {
            prop.position += new Vector3(0, 0, State.TrainSpeed * Time.deltaTime);
            if (prop.position.z < -52) prop.position = new Vector3(prop.position.x, 0, (SceneryProps.childCount - 1) * 5.4f);
        }
    }
}

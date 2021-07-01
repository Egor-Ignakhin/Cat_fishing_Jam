using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoplavokController : MonoBehaviour
{
    private float minY;
    private float maxY;
    [SerializeField] private float range;
    [Range(0, 10)] [SerializeField] private float speed;
    private bool lastYIsMax = false;
    [SerializeField] Transform mainT;

    private void Awake()
    {
        minY = mainT.position.y + -range / 2;
        maxY = mainT.position.y + range / 2;        
    }
    void Update()
    {
        if (mainT.position.y >= maxY)
        {
            lastYIsMax = true;
        }
        if (mainT.position.y <= minY)
        {
            lastYIsMax = false;
        }
        mainT.position += new Vector3(0, (lastYIsMax ? -Time.deltaTime : Time.deltaTime) * speed * (range / 1) * 0.5F, 0);
    }

    internal Vector3 GetMainTPos()
    {
        return mainT.position;
    }

    internal void ReturnAnimation()
    {
        speed = 1;
    }

    internal void Call()
    {
        speed = 5;
    }

    internal Transform GetMain()
    {
        return mainT;
    }
}

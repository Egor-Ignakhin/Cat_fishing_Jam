using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotController : MonoBehaviour
{
    private float minY;
    private float maxY;
    [SerializeField] private float range;
    [Range(0, 10)] [SerializeField] private float speed;
    private bool lastYIsMax = false;
    private void Awake()
    {
        minY = transform.position.y + -range / 2;
        maxY = transform.position.y + range / 2;
    }
    void Update()
    {
        if (transform.position.y >= maxY)
        {
            lastYIsMax = true;
        }
        if (transform.position.y <= minY)
        {
            lastYIsMax = false;
        }
        transform.position += new Vector3(0, (lastYIsMax ? -Time.deltaTime : Time.deltaTime) * speed * (range / 1), 0);
    }
}

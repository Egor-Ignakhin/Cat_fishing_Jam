using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public Vector3 target;
    void Update()
    {
        transform.LookAt(target);
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime);
        if (Vector3.Distance(transform.position, target) < 1)
            Destroy(gameObject);
    }
}

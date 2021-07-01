using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CatController : MonoBehaviour
{
    [SerializeField] private GameObject line;

    internal async void StartLine()
    {
        await Task.Delay(2200);
        line.SetActive(true);
    }
    public async void Back()
    {
        line.SetActive(false);
        await Task.Delay(2400);
        GetComponent<Animator>().Play("Start");
        StartLine();        
    }
}

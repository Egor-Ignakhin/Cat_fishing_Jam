using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Shop : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject cImage;

    private float money = 0;
    [SerializeField] private TextMeshProUGUI moneytxt;
    private FinishAnimator finishAnimator;
    [SerializeField] private List<Texture> texturesPoplavok = new List<Texture>();
    [SerializeField] private GameObject poplavok;
    private void Start()
    {
        UpdateMoneyTxt();
        cImage.SetActive(false);
        finishAnimator = FindObjectOfType<FinishAnimator>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (cImage.activeInHierarchy)
            {
                cImage.SetActive(false);
            }
        }
    }
    public void AddMoney(float v)
    {
        money += v;
        UpdateMoneyTxt();
    }
    internal void Buy(float cost, int i)
    {
        if (i < 14)
        {         
            if (money >= cost)
            {
                money -= cost;
                UpdateMoneyTxt();
                poplavok.GetComponent<MeshRenderer>().material.mainTexture = texturesPoplavok[i];
            }
        }
        else
        {
            finishAnimator.Animate();
            FishTimer.IsEnd = true;
            cImage.SetActive(false);
        }
    }

    private void UpdateMoneyTxt()
    {
        moneytxt.text = $"Money: {money}";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        cImage.SetActive(!cImage.activeInHierarchy);
    }
}

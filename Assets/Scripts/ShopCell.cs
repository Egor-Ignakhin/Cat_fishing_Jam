using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopCell : MonoBehaviour, IPointerClickHandler
{
    private Shop shop;
    public int i;
    [SerializeField] private float cost = 100;
    public void OnPointerClick(PointerEventData eventData)
    {
        shop.Buy(cost, i);
    }

    private void Awake()
    {
        shop = FindObjectOfType<Shop>();
    }
}

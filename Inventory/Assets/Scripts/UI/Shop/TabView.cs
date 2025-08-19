using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TabView : MonoBehaviour, IPointerClickHandler
{
    public TextMeshProUGUI _name;
    public UnityEngine.UI.Image icon;
    public TextMeshProUGUI rarity;
    public TextMeshProUGUI buyingPrice;
    private ShopView shopView;

    public void Initialize(ShopView shopView)
    {
        this.shopView = shopView;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        shopView.FillItemInfo(_name.text);
    }
}

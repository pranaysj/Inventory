using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameService;

public class EventService
{
    public event Action OnInventoryKeyPressed;
    public event Action<TabView> OnClikedIPlayerItem, OnClikedIShopItem, OnClickedAnotehrItem;

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            OnInventoryKeyPressed?.Invoke();
            ServiceLocator.Get<SoundService>().PlaySoundEffects(SoundType.KeyboardClick);
        }
    }

    public void ClickedItem(TabView view)
    {
        OnClickedAnotehrItem?.Invoke(view);

        TabType type = view.TabType;

        if (type == TabType.Player)
            OnClikedIPlayerItem?.Invoke(view);

        if (type == TabType.Shop)
            OnClikedIShopItem?.Invoke(view);
    }
}

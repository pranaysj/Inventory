using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static GameService;

public class EventService
{
    private SoundService soundService;
    public event Action OnInventoryKeyPressed;
    public event Action<TabView> OnClickedPlayerItem, OnClickedShopItem, OnClickedAnotherItem;

    public EventService(SoundService soundService)
    {
        this.soundService = soundService;
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            OnInventoryKeyPressed?.Invoke();
            soundService?.PlaySoundEffects(SoundType.KeyboardClick);
        }
    }

    public void ClickedItem(TabView view)
    {
        OnClickedAnotherItem?.Invoke(view);

        TabType type = view.TabType;

        if (type == TabType.Player)
            OnClickedPlayerItem?.Invoke(view);

        if (type == TabType.Shop)
            OnClickedShopItem?.Invoke(view);
    }
}

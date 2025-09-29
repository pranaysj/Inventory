using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class InventoryController 
{
    private readonly GameObject inventoryPanel;
    private readonly EventService eventService;

    public InventoryController(GameObject inventoryPanel, EventService eventService)
    {
        this.inventoryPanel = inventoryPanel;
        this.eventService = eventService;

        this.eventService.OnInventoryKeyPressed += ToggleInventoryPanel;
        this.inventoryPanel.SetActive(false);
    }

    ~InventoryController()
    {
        eventService.OnInventoryKeyPressed -= ToggleInventoryPanel; 
    }

    private void ToggleInventoryPanel()
    {
        if (inventoryPanel != null)
            inventoryPanel.SetActive(!inventoryPanel.activeInHierarchy);
    }
}

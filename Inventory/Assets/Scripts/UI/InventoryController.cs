using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private GameObject inventoryPanel;
    private EventService eventService;

    private void Start()
    {
        inventoryPanel = ServiceLocator.Get<GameService>().InventoryPanel;
        eventService = ServiceLocator.Get<EventService>();

        eventService.OnInventoryKeyPressed += ToggleInventoryPanel;


        if (inventoryPanel != null)
            inventoryPanel.SetActive(false); // Ensure the inventory panel is initially hidden
    }

    private void OnDestroy()
    {
        eventService.OnInventoryKeyPressed -= ToggleInventoryPanel; 
    }

    private void ToggleInventoryPanel()
    {
        if (inventoryPanel != null)
            inventoryPanel.SetActive(!inventoryPanel.activeInHierarchy);
    }
}

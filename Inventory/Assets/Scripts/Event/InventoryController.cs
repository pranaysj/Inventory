using UnityEngine;

public class InventoryController
{
    private readonly GameObject inventoryPanel;
    private readonly EventService eventService;

    public InventoryController(GameObject inventoryPanel, EventService eventService)
    {
        this.inventoryPanel = inventoryPanel ?? throw new System.ArgumentNullException(nameof(inventoryPanel));
        this.eventService = eventService ?? throw new System.ArgumentNullException(nameof(eventService));

        SubscribeEvents();
        HideInventoryPanel();
    }

    private void SubscribeEvents()
    {
        eventService.OnInventoryKeyPressed += ToggleInventoryPanel;
    }

    private void UnsubscribeEvents()
    {
        eventService.OnInventoryKeyPressed -= ToggleInventoryPanel;
    }

    private void HideInventoryPanel()
    {
        inventoryPanel.SetActive(false);
    }

    private void ToggleInventoryPanel()
    {
        if (inventoryPanel != null)
        {
            bool isActive = inventoryPanel.activeSelf;
            inventoryPanel.SetActive(!isActive);
        }
    }

    // Call this explicitly from GameService.OnDestroy or similar lifecycle hook
    public void Dispose()
    {
        UnsubscribeEvents();
    }
}
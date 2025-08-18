using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TabController : MonoBehaviour
{
    [SerializeField] private GameObject[] tabPanels;
    [SerializeField] private TextMeshProUGUI[] tabsButton;

    private Color activeColor = new Color32(220,219,218,225);
    private Color inactiveColor = new Color32(115, 115, 115,225);


    private void Start()
    {
        tabPanels = ServiceLocator.Get<GameService>().TabPanel;
        tabsButton = ServiceLocator.Get<GameService>().TabButton;
        Switch(0);
    }

    public void Switch(int tabID)
    {
        if(tabPanels == null || tabsButton == null) return;
        if (tabID < 0 || tabID >= tabPanels.Length || tabID >= tabsButton.Length) return;

        foreach (var panel in tabPanels)
        {
            if (panel != null)
                panel.SetActive(false);
        }

        foreach (var button in tabsButton)
        {
            if (button != null)
                button.color = inactiveColor;
        }

        if (tabPanels[tabID] != null)
            tabPanels[tabID].SetActive(true);
        if (tabsButton[tabID] != null) 
            tabsButton[tabID].color = activeColor;
    }
}

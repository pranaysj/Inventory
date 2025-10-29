using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopDatabase", menuName = "Shop/DatabaseSO")]
public class ShopDatabaseSO : ScriptableObject
{ 
    public List<ShopItemSO> shopItems;
    private int randomItem;

    //get the item based on item rarity
    public ShopItemSO GetRandomItemByRarity(Rarity rarity)
    {
        List<ShopItemSO> filteredItems = shopItems.FindAll(item => item.rarity == rarity);
        if (filteredItems.Count == 0)
        {
            return null; // No items found with the specified rarity
        }
        randomItem = Random.Range(0, filteredItems.Count);
        return filteredItems[randomItem];
    }
}

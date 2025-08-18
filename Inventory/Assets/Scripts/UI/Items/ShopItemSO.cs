using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "ShopItem", menuName = "Shop/Item")]
public class ShopItemSO : ScriptableObject
{
    public ItemType itemType;
    public string itemName;
    public Sprite icon;
    public string description;
    public int buyingPrice;
    public int sellingPrice;
    public int weight;
    public Rarity rarity;
}

public enum ItemType
{
    Material,
    Weapons,
    Consumables,
    Treasure
}

public enum Rarity
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary
}
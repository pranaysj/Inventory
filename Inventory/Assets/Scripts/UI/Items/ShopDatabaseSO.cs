using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopDatabase", menuName = "Shop/Database")]
public class ShopDatabaseSO : ScriptableObject
{ 
    public List<ShopItemSO> shopItems;
}

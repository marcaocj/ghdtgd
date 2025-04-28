using UnityEngine;

[CreateAssetMenu(fileName = "NewShopItem", menuName = "RPG/ShopItem", order = 1)]
public class ShopItem : ScriptableObject
{
    public Item item;
    public int price;
}

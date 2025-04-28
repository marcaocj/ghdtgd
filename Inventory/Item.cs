using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "RPG/Item", order = 1)]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public ItemType type;
    public int maxStack = 1;

    [Header("Equipment Bonuses")] 
    public int bonusSTR, bonusAGI, bonusVIT, bonusINT, bonusDEX, bonusLUK;

    [Header("Durability (Equipment)")]
    public int maxDurability = 100;
}

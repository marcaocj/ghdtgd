using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Game.Stats;

namespace Game.Inventory
{
    /// <summary>
    /// Manages equipping items and applies their stat bonuses to the player.
    /// </summary>
    public class EquipmentManager : MonoBehaviour
    {
        public CharacterStatsData stats;
        private List<Item> equippedItems = new List<Item>();

        [Header("Events")]
        public UnityEvent OnEquipmentChanged;

        /// <summary>Equip an item, adding its bonuses to CharacterStats.</summary>
        public void Equip(Item item)
        {
            if (item == null || equippedItems.Contains(item)) return;
            equippedItems.Add(item);
            stats.baseSTR  += item.bonusSTR;
            stats.baseAGI  += item.bonusAGI;
            stats.baseVIT  += item.bonusVIT;
            stats.baseINT  += item.bonusINT;
            stats.baseDEX  += item.bonusDEX;
            stats.baseLUK  += item.bonusLUK;
            stats.OnLevelUp?.Invoke(); // refresh derived stats/UI
            OnEquipmentChanged?.Invoke();
        }

        /// <summary>Unequip an item, removing its bonuses.</summary>
        public void Unequip(Item item)
        {
            if (item == null || !equippedItems.Contains(item)) return;
            equippedItems.Remove(item);
            stats.baseSTR  -= item.bonusSTR;
            stats.baseAGI  -= item.bonusAGI;
            stats.baseVIT  -= item.bonusVIT;
            stats.baseINT  -= item.bonusINT;
            stats.baseDEX  -= item.bonusDEX;
            stats.baseLUK  -= item.bonusLUK;
            stats.OnLevelUp?.Invoke();
            OnEquipmentChanged?.Invoke();
        }

        /// <summary>Get list of currently equipped items.</summary>
        public IReadOnlyList<Item> GetEquippedItems() => equippedItems.AsReadOnly();
    }
}

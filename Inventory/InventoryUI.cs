using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    public GameObject slotPrefab;
    public Transform slotParent;
    public Inventory inventory;

    [Header("Category Tabs")]
    public Button btnAll;
    public Button btnEquipment;
    public Button btnConsumable;
    public Button btnOther;
    private List<Slot> slots = new List<Slot>();
    private ItemType? currentFilter = null;

    private void Start()
    {
        for (int i = 0; i < inventory.capacity; i++)
        {
            var go = Instantiate(slotPrefab, slotParent);
            var slot = go.GetComponent<Slot>();
            slot.index = i;
            slot.inventory = inventory;
            slots.Add(slot);
        }
        // setup tab buttons
        if (btnAll) btnAll.onClick.AddListener(() => ApplyFilter(null));
        if (btnEquipment) btnEquipment.onClick.AddListener(() => ApplyFilter(ItemType.Equipment));
        if (btnConsumable) btnConsumable.onClick.AddListener(() => ApplyFilter(ItemType.Consumable));
        if (btnOther) btnOther.onClick.AddListener(() => ApplyFilter(ItemType.Other));
        ApplyFilter(null);
    }
    // Show/hide slots based on selected category
    private void ApplyFilter(ItemType? filter)
    {
        currentFilter = filter;
        foreach (var slot in slots)
        {
            var item = inventory.items[slot.index];
            bool show = !filter.HasValue || (item != null && item.type == filter.Value);
            slot.gameObject.SetActive(show);
        }
    }
}

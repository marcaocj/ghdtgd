using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int capacity = 20;
    public List<Item> items = new List<Item>();
    public List<int> itemCounts = new List<int>();

    void Awake()
    {
        // initialize fixed-size inventory
        for (int i = items.Count; i < capacity; i++)
        {
            items.Add(null);
            itemCounts.Add(0);
        }
    }

    public bool AddItem(Item item)
    {
        // stack in existing slot
        if (item.maxStack > 1)
        {
            for (int i = 0; i < capacity; i++)
            {
                if (items[i] == item && itemCounts[i] < item.maxStack)
                {
                    itemCounts[i]++;
                    return true;
                }
            }
        }
        // find empty slot
        for (int i = 0; i < capacity; i++)
        {
            if (items[i] == null)
            {
                items[i] = item;
                itemCounts[i] = 1;
                return true;
            }
        }
        return false;
    }

    public void RemoveItem(int index)
    {
        if (index < 0 || index >= capacity) return;
        if (items[index] == null) return;
        itemCounts[index]--;
        if (itemCounts[index] <= 0)
        {
            items[index] = null;
            itemCounts[index] = 0;
        }
    }

    // swap items between slots
    public void SwapItems(int indexA, int indexB)
    {
        if (indexA < 0 || indexA >= capacity || indexB < 0 || indexB >= capacity) return;
        var tempItem = items[indexA];
        var tempCount = itemCounts[indexA];
        items[indexA] = items[indexB];
        itemCounts[indexA] = itemCounts[indexB];
        items[indexB] = tempItem;
        itemCounts[indexB] = tempCount;
    }
}

using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

/// <summary>
/// Manages buying and selling items with player.
/// </summary>
public class ShopManager : MonoBehaviour
{
    [Header("Stock Items")]
    public List<ShopItem> stock;

    [HideInInspector] public UnityEvent<ShopItem> OnPurchaseSuccess;
    [HideInInspector] public UnityEvent<ShopItem> OnPurchaseFail;

    private Inventory inventory;
    private Currency currency;

    void Awake()
    {
        inventory = Object.FindFirstObjectByType<Inventory>();
        currency = Object.FindFirstObjectByType<Currency>();
        if (OnPurchaseSuccess == null) OnPurchaseSuccess = new UnityEvent<ShopItem>();
        if (OnPurchaseFail == null) OnPurchaseFail = new UnityEvent<ShopItem>();
    }

    /// <summary>
    /// Attempt to purchase an item. Returns true if successful.
    /// </summary>
    public bool Purchase(ShopItem shopItem)
    {
        if (currency.Spend(shopItem.price))
        {
            if (inventory.AddItem(shopItem.item))
            {
                OnPurchaseSuccess.Invoke(shopItem);
                return true;
            }
            // inventory full, refund
            currency.Earn(shopItem.price);
        }
        OnPurchaseFail.Invoke(shopItem);
        return false;
    }

    /// <summary>
    /// Sell an item to shop at half price.
    /// </summary>
    public bool Sell(Item item)
    {
        int sellPrice = Mathf.FloorToInt(item.maxStack > 1 ? item.maxStack : 1) * 1; // or define per item
        // find item index and remove by index
        int index = inventory.items.IndexOf(item);
        if (index >= 0)
        {
            inventory.RemoveItem(index);
            currency.Earn(sellPrice);
            return true;
        }
        return false;
    }
}

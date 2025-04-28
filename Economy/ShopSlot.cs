using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    public Image icon;
    public Text nameText;
    public Text priceText;
    public Button buyButton;

    private ShopItem shopItem;
    private ShopManager shopManager;

    public void Init(ShopItem item, ShopManager manager)
    {
        shopItem = item;
        shopManager = manager;
        icon.sprite = item.item.icon;
        nameText.text = item.item.itemName;
        priceText.text = item.price.ToString() + " G";
        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(() => shopManager.Purchase(shopItem));
    }
}

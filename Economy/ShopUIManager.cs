using UnityEngine;
using UnityEngine.UI;

public class ShopUIManager : MonoBehaviour
{
    public ShopManager shopManager;
    public Transform contentParent;
    public GameObject shopSlotPrefab;
    public Text feedbackText;

    void Start()
    {
        PopulateShop();
        shopManager.OnPurchaseSuccess.AddListener(OnSuccess);
        shopManager.OnPurchaseFail.AddListener(OnFail);
    }

    void PopulateShop()
    {
        foreach (Transform child in contentParent)
            Destroy(child.gameObject);
        foreach (var item in shopManager.stock)
        {
            var slotObj = Instantiate(shopSlotPrefab, contentParent);
            var slot = slotObj.GetComponent<ShopSlot>();
            if (slot != null)
                slot.Init(item, shopManager);
        }
    }

    void OnSuccess(ShopItem shopItem)
    {
        if (feedbackText) feedbackText.text = "Comprado: " + shopItem.item.itemName;
    }

    void OnFail(ShopItem shopItem)
    {
        if (feedbackText) feedbackText.text = "Falha ao comprar: " + shopItem.item.itemName;
    }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquipmentSlot : MonoBehaviour, IPointerClickHandler
{
    public Image icon;
    public Text durabilityText;
    private Item item;
    private int currentDurability;

    public void SetItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
        if (item.maxDurability > 0)
        {
            currentDurability = item.maxDurability;
            durabilityText.text = currentDurability + "/" + item.maxDurability;
            durabilityText.enabled = true;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (item != null && item.maxDurability > 0)
        {
            // Simulate use/wear
            currentDurability--;
            durabilityText.text = currentDurability + "/" + item.maxDurability;
            if (currentDurability <= 0)
            {
                // break item
                icon.enabled = false;
                durabilityText.enabled = false;
                item = null;
            }
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Slot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public int index;
    public Inventory inventory;
    public Image icon;

    private GameObject dragIcon;
    private RectTransform dragRect;

    void Start()
    {
        UpdateSlot();
    }

    public void UpdateSlot()
    {
        var item = inventory.items[index];
        if (item != null)
        {
            icon.sprite = item.icon;
            icon.enabled = true;
        }
        else icon.enabled = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (inventory.items[index] == null) return;
        dragIcon = new GameObject("DragIcon");
        dragIcon.transform.SetParent(transform.root, false);
        var img = dragIcon.AddComponent<Image>();
        img.sprite = icon.sprite;
        img.raycastTarget = false;
        dragRect = dragIcon.GetComponent<RectTransform>();
        dragRect.sizeDelta = icon.rectTransform.sizeDelta;
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (dragIcon != null)
            dragRect.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (dragIcon != null)
        {
            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);
            foreach (var res in results)
            {
                var dst = res.gameObject.GetComponent<Slot>();
                if (dst != null && dst.inventory == inventory)
                {
                    inventory.SwapItems(index, dst.index);
                    dst.UpdateSlot();
                    UpdateSlot();
                    break;
                }
            }
            Destroy(dragIcon);
        }
    }
}

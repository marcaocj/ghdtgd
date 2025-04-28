using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// Manages the skill bar UI: populates slots and assigns click actions.
/// </summary>
public class SkillUIManager : MonoBehaviour
{
    public SkillManager skillManager;
    public GameObject skillSlotPrefab;
    public Transform slotParent;
    private List<SkillSlot> slots = new List<SkillSlot>();

    void Start()
    {
        if (skillManager == null) skillManager = Object.FindFirstObjectByType<SkillManager>();
        // clear existing
        foreach (Transform c in slotParent) Destroy(c.gameObject);
        // create slots
        for (int i = 0; i < skillManager.skills.Count; i++)
        {
            var go = Instantiate(skillSlotPrefab, slotParent);
            var slot = go.GetComponent<SkillSlot>();
            slot.index = i;
            slotManagerSetup(slot);
            slots.Add(slot);
        }
    }

    private void slotManagerSetup(SkillSlot slot)
    {
        // nothing extra for now; SkillSlot finds manager automatically
    }
}

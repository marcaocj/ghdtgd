using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillSlot : MonoBehaviour, IPointerClickHandler
{
    public Image icon;
    public Image cooldownOverlay;
    public int index;

    private Skill skill;
    private SkillManager manager;

    void Start()
    {
        manager = Object.FindFirstObjectByType<SkillManager>();
        if (manager != null && index >= 0 && index < manager.skills.Count)
        {
            skill = manager.skills[index];
            icon.sprite = skill.icon;
            icon.enabled = true;
        }
        else icon.enabled = false;
    }

    void Update()
    {
        if (skill != null)
        {
            float cd = manager.GetCooldown(skill);
            if (cd > 0f)
            {
                cooldownOverlay.fillAmount = cd / skill.cooldown;
                cooldownOverlay.enabled = true;
            }
            else
            {
                cooldownOverlay.fillAmount = 0f;
                cooldownOverlay.enabled = false;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (skill != null)
            manager.UseSkill(index);
    }
}

using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "RPG/Skill", order = 1)]
public class Skill : ScriptableObject
{
    public string skillName;
    public SkillType type;
    public Sprite icon;
    public float damage;
    public float spCost;
    public float cooldown;
    [Header("VFX (Optional)")]
    public GameObject vfxPrefab;
    public float castTime;
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillManager : MonoBehaviour
{
    public List<Skill> skills = new List<Skill>();
    private Dictionary<string, float> cooldowns = new Dictionary<string, float>();
    private Mana mana;
    [HideInInspector] public GameObject currentTarget;

    void Start()
    {
        mana = GetComponent<Mana>();
        foreach (var skill in skills)
            cooldowns[skill.skillName] = 0f;
    }

    void Update()
    {
        var keys = new List<string>(cooldowns.Keys);
        foreach (var key in keys)
            if (cooldowns[key] > 0f)
                cooldowns[key] -= Time.deltaTime;
    }

    public bool CanUse(Skill skill)
    {
        return cooldowns[skill.skillName] <= 0f && mana.currentMana >= skill.spCost;
    }

    public void UseSkill(int index, GameObject target = null)
    {
        if (index < 0 || index >= skills.Count) return;
        var skill = skills[index];
        if (!CanUse(skill)) return;
        // use selected target if none specified
        if (target == null) target = currentTarget;
        StartCoroutine(CastRoutine(skill, target));
    }

    private IEnumerator CastRoutine(Skill skill, GameObject target)
    {
        yield return new WaitForSeconds(skill.castTime);
        mana.UseMana(skill.spCost);
        ExecuteSkill(skill, target);
        cooldowns[skill.skillName] = skill.cooldown;
    }

    private void ExecuteSkill(Skill skill, GameObject target)
    {
        // apply damage
        if (target != null)
        {
            var health = target.GetComponent<Health>();
            if (health != null)
                health.TakeDamage(skill.damage);
            // spawn VFX at target
            if (skill.vfxPrefab != null)
            {
                var vfxObj = Instantiate(skill.vfxPrefab, target.transform.position, Quaternion.identity);
                Destroy(vfxObj, 3f);
            }
        }
        else
        {
            // spawn VFX at player if no target
            if (skill.vfxPrefab != null)
            {
                var vfxObj = Instantiate(skill.vfxPrefab, transform.position, Quaternion.identity);
                Destroy(vfxObj, 3f);
            }
        }
    }

    public float GetCooldown(Skill skill)
    {
        return Mathf.Max(0f, cooldowns[skill.skillName]);
    }
}

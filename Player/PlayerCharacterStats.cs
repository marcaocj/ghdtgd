namespace Game.Player {
    using UnityEngine;
    using Game.Buffs;
    using System.Collections.Generic;

    [System.Serializable]
    public class PlayerCharacterStats : MonoBehaviour
    {
        [Header("Base Attributes")]
        public int STR = 10;
        public int AGI = 10;
        public int VIT = 10;
        public int INT = 10;
        public int DEX = 10;
        public int LUK = 10;

        [Header("Derived Stats")]    
        public float Attack { get { return (STR + buffAdditions[CharacterStatType.STR]) * 2f; } }
        public float Defense { get { return (VIT + buffAdditions[CharacterStatType.VIT]) * 1.5f; } }
        public float MagicAttack { get { return (INT + buffAdditions[CharacterStatType.INT]) * 2f; } }
        public float Dodge { get { return (AGI + buffAdditions[CharacterStatType.AGI]) * 0.5f; } }
        public float CriticalRate { get { return (LUK + buffAdditions[CharacterStatType.LUK]) * 0.2f; } }

        [Header("Health & Mana")]
        public float maxHP { get { return (VIT + buffAdditions[CharacterStatType.VIT]) * 10f; } }
        public float maxSP { get { return (INT + buffAdditions[CharacterStatType.INT]) * 5f + (DEX + buffAdditions[CharacterStatType.DEX]) * 2f; } }

        [HideInInspector] public int level = 1;
        [HideInInspector] public float currentXP = 0f;
        public float xpToNextLevel = 100f;
        public int statPointsPerLevel = 5;

        private Dictionary<CharacterStatType, float> buffAdditions;

        void Awake()
        {
            // initialize buff additions early
            buffAdditions = new Dictionary<CharacterStatType, float>();
            foreach (CharacterStatType t in System.Enum.GetValues(typeof(CharacterStatType)))
                buffAdditions[t] = 0f;
        }

        void Start()
        {
            // initialize health and mana (buffAdditions ready in Awake)
            var healthComp = GetComponent<Health>();
            if (healthComp != null)
                healthComp.Initialize(maxHP);
            var manaComp = GetComponent<Mana>();
            if (manaComp != null)
                manaComp.Initialize(maxSP);
        }

        public void GainXP(float amount)
        {
            currentXP += amount;
            while (currentXP >= xpToNextLevel)
            {
                currentXP -= xpToNextLevel;
                LevelUp();
            }
        }

        public void ApplyBuff(Buff buff)
        {
            buffAdditions[buff.statType] += buff.value;
        }

        public void RemoveBuff(Buff buff)
        {
            buffAdditions[buff.statType] -= buff.value;
        }

        private void LevelUp()
        {
            level++;
            xpToNextLevel *= 1.1f;
            // distribute stat points (for now auto all to STR)
            STR += statPointsPerLevel;
            // TODO: prompt player to allocate points
            Debug.Log($"Leveled up to {level}! STR increased by {statPointsPerLevel}.");

            // update health and mana
            var healthComp = GetComponent<Health>();
            if (healthComp != null)
                healthComp.SetMaxHealth(maxHP);
            var manaComp = GetComponent<Mana>();
            if (manaComp != null)
                manaComp.SetMaxMana(maxSP);
        }
    }
}

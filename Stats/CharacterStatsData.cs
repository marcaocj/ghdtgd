using UnityEngine;
using UnityEngine.Events;

namespace Game.Stats
{
    public enum AttributeType { STR, AGI, VIT, INT, DEX, LUK }

    public class CharacterStatsData : MonoBehaviour
    {
        [Header("Base Attributes")]
        public int baseSTR, baseAGI, baseVIT, baseINT, baseDEX, baseLUK;

        [Header("Level")]
        public int level = 1;
        public int currentXP = 0;
        public int expToLevelUp = 100;
        public int availablePoints = 0;
        public int pointsPerLevel = 5;

        [Header("Current SP")]
        public int currentSP;

        public UnityEvent OnLevelUp;

        // Derived stats
        public int Attack => baseSTR * 2 + baseDEX;
        public int Defense => baseVIT * 2 + baseAGI;
        public int MagicAttack => baseINT * 2;
        public float Dodge => baseAGI * 0.1f;
        public float Critical => baseLUK * 0.1f;

        public int MaxHP => baseVIT * 10;
        public int MaxSP => baseINT * 5;

        void Start()
        {
            currentXP = 0;
            availablePoints = 0;
            currentSP = MaxSP;
        }

        public void AddXP(int xp)
        {
            currentXP += xp;
            while (currentXP >= expToLevelUp)
            {
                currentXP -= expToLevelUp;
                LevelUp();
            }
        }

        void LevelUp()
        {
            level++;
            expToLevelUp = Mathf.FloorToInt(expToLevelUp * 1.1f);
            availablePoints += pointsPerLevel;
            OnLevelUp?.Invoke();
        }

        public bool AllocatePoint(AttributeType type)
        {
            if (availablePoints <= 0) return false;
            availablePoints--;
            switch (type)
            {
                case AttributeType.STR: baseSTR++; break;
                case AttributeType.AGI: baseAGI++; break;
                case AttributeType.VIT: baseVIT++; break;
                case AttributeType.INT: baseINT++; break;
                case AttributeType.DEX: baseDEX++; break;
                case AttributeType.LUK: baseLUK++; break;
            }
            return true;
        }
    }
}

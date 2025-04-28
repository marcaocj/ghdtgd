using UnityEngine;

namespace Game.Buffs
{
    [CreateAssetMenu(fileName = "NewBuff", menuName = "RPG/Buff", order = 1)]
    public class Buff : ScriptableObject
    {
        public string buffName;
        public Sprite icon;
        public CharacterStatType statType;
        public float value;      // additive value
        public float duration;   // seconds
    }
}

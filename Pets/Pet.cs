using UnityEngine;
using Game.Buffs;

namespace Game.Pets
{
    [CreateAssetMenu(fileName = "NewPetData", menuName = "RPG/PetData", order = 1)]
    public class PetData : ScriptableObject
    {
        public string petName;
        public int petId;
        public Sprite petIcon;
        public GameObject petPrefab;
        public float followSpeed = 3f;
        public float followDistance = 2f;
        public float lootRange = 1.5f;
        public Buff[] buffs;
        public SkillType[] skills;
    }
}

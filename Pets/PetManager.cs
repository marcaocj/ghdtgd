using UnityEngine;
using Game.Pets;
using Game.Player;
using Game.Buffs;

namespace Game.Pets
{
    /// <summary>
    /// Instantiates and manages the active pet for the player.
    /// </summary>
    public class PetManager : MonoBehaviour
    {
        public PetData petData;
        private GameObject currentPet;
        private PetController controller;

        void Start()
        {
            if (petData != null)
            {
                currentPet = Instantiate(petData.petPrefab);
                controller = currentPet.GetComponent<PetController>();
                controller.player = transform;
                controller.followSpeed = petData.followSpeed;
                controller.followDistance = petData.followDistance;
                controller.lootRange = petData.lootRange;

                // Apply pet buffs
                var stats = GetComponent<PlayerCharacterStats>();
                foreach (var buff in petData.buffs)
                    stats.ApplyBuff(buff);
            }
        }
    }
}

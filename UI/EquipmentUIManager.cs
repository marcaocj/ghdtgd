using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Inventory;
using Game.Stats;

namespace Game.UI
{
    /// <summary>
    /// Updates stats display and equipped items UI.
    /// </summary>
    public class EquipmentUIManager : MonoBehaviour
    {
        [Header("References")]
        public EquipmentManager equipmentManager;
        public CharacterStatsData characterStats;

        [Header("Stats UI")]
        public Text strText;
        public Text agiText;
        public Text vitText;
        public Text intText;
        public Text dexText;
        public Text lukText;
        public Text attackText;
        public Text defenseText;
        public Text magicAttackText;
        public Text dodgeText;
        public Text critText;
        public Text hpText;
        public Text spText;

        [Header("Equipped Slots")]
        public List<EquipmentSlot> equipmentSlots;

        private Health health;

        void Start()
        {
            if (equipmentManager != null)
                equipmentManager.OnEquipmentChanged.AddListener(UpdateEquipmentUI);
            if (characterStats != null)
            {
                health = characterStats.GetComponent<Health>();
                characterStats.OnLevelUp.AddListener(UpdateStatsUI);
            }

            UpdateStatsUI();
            UpdateEquipmentUI();
        }

        void UpdateStatsUI()
        {
            if (characterStats == null) return;
            strText.text = characterStats.baseSTR.ToString();
            agiText.text = characterStats.baseAGI.ToString();
            vitText.text = characterStats.baseVIT.ToString();
            intText.text = characterStats.baseINT.ToString();
            dexText.text = characterStats.baseDEX.ToString();
            lukText.text = characterStats.baseLUK.ToString();

            attackText.text = characterStats.Attack.ToString();
            defenseText.text = characterStats.Defense.ToString();
            magicAttackText.text = characterStats.MagicAttack.ToString();
            dodgeText.text = (characterStats.Dodge * 100f).ToString("F1") + "%";
            critText.text = (characterStats.Critical * 100f).ToString("F1") + "%";
            var currentHealth = health != null ? health.currentHealth : characterStats.MaxHP;
            hpText.text = currentHealth + "/" + characterStats.MaxHP;
            spText.text = characterStats.currentSP + "/" + characterStats.MaxSP;
        }

        void UpdateEquipmentUI()
        {
            if (equipmentManager == null) return;
            var items = equipmentManager.GetEquippedItems();
            for (int i = 0; i < equipmentSlots.Count; i++)
            {
                var slot = equipmentSlots[i];
                if (i < items.Count && items[i] != null)
                    slot.SetItem(items[i]);
                else
                {
                    slot.GetComponent<Image>().enabled = false;
                    slot.durabilityText.enabled = false;
                }
            }
        }
    }
}

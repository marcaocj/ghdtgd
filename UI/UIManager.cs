using UnityEngine;
using UnityEngine.UI;
using Game.Player;

namespace Game.UI
{
    public class UIManager : MonoBehaviour
    {
        public Slider healthBar;
        public Slider manaBar;
        public Slider xpBar;
        public Text levelText;

        private Health health;
        private Mana mana;
        private PlayerCharacterStats stats;

        void Start()
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            health = player.GetComponent<Health>();
            mana = player.GetComponent<Mana>();
            stats = player.GetComponent<PlayerCharacterStats>();

            healthBar.maxValue = health.maxHealth;
            manaBar.maxValue = mana.maxMana;
            xpBar.maxValue = stats.xpToNextLevel;
            UpdateLevelText();
        }

        void Update()
        {
            healthBar.value = health.currentHealth;
            manaBar.value = mana.currentMana;
            xpBar.value = stats.currentXP;
        }

        public void UpdateLevelText()
        {
            if (levelText)
                levelText.text = $"Lv {stats.level}";
        }
    }
}

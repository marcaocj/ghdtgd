using UnityEngine;
using UnityEngine.UI;
using Game.Buffs;

namespace Game.Buffs
{
    public class BuffSlot : MonoBehaviour
    {
        public Image icon;
        public Image timerOverlay;
        private Buff buff;

        public void SetBuff(Buff b)
        {
            buff = b;
            icon.sprite = buff.icon;
            icon.enabled = true;
            timerOverlay.fillAmount = 1f;
            timerOverlay.enabled = true;
        }

        public void UpdateTimer(float normalized)
        {
            // normalized: 0 to 1 remaining
            timerOverlay.fillAmount = normalized;
        }
    }
}

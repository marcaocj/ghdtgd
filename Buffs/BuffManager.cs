using UnityEngine;
using System.Collections.Generic;
using Game.Buffs;
using Game.Player;

namespace Game.Buffs
{
    public class BuffManager : MonoBehaviour
    {
        public GameObject buffIconPrefab;
        public Transform buffIconParent;

        private PlayerCharacterStats stats;
        private Dictionary<Buff, float> activeBuffs = new Dictionary<Buff, float>();
        private Dictionary<Buff, GameObject> iconMap = new Dictionary<Buff, GameObject>();

        void Start()
        {
            stats = GetComponent<PlayerCharacterStats>();
        }

        void Update()
        {
            var toRemove = new List<Buff>();
            foreach (var kv in activeBuffs)
            {
                activeBuffs[kv.Key] -= Time.deltaTime;
                if (activeBuffs[kv.Key] <= 0f)
                    toRemove.Add(kv.Key);
                else
                {
                    // update UI overlay
                    var iconObj = iconMap[kv.Key];
                    var slot = iconObj.GetComponent<BuffSlot>();
                    if (slot) slot.UpdateTimer(activeBuffs[kv.Key] / kv.Key.duration);
                }
            }
            foreach (var buff in toRemove)
                RemoveBuff(buff);
        }

        public void AddBuff(Buff buff)
        {
            if (activeBuffs.ContainsKey(buff))
            {
                activeBuffs[buff] = buff.duration; // reset duration
            }
            else
            {
                activeBuffs.Add(buff, buff.duration);
                stats.ApplyBuff(buff);
                var iconObj = Instantiate(buffIconPrefab, buffIconParent);
                var slot = iconObj.GetComponent<BuffSlot>();
                if (slot) slot.SetBuff(buff);
                iconMap.Add(buff, iconObj);
            }
        }

        public void RemoveBuff(Buff buff)
        {
            if (!activeBuffs.ContainsKey(buff)) return;
            stats.RemoveBuff(buff);
            Destroy(iconMap[buff]);
            activeBuffs.Remove(buff);
            iconMap.Remove(buff);
        }
    }
}

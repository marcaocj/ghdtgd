using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using Game.Player;

public class QuestManager : MonoBehaviour
{
    [Header("Quests")]
    public List<Quest> availableQuests = new List<Quest>();
    [HideInInspector] public List<Quest> activeQuests = new List<Quest>();

    public UnityEvent<Quest> OnQuestAccepted;
    public UnityEvent<Quest> OnQuestUpdated;
    public UnityEvent<Quest> OnQuestCompleted;

    void Start()
    {
        if (OnQuestAccepted == null) OnQuestAccepted = new UnityEvent<Quest>();
        if (OnQuestUpdated == null) OnQuestUpdated = new UnityEvent<Quest>();
        if (OnQuestCompleted == null) OnQuestCompleted = new UnityEvent<Quest>();
    }

    // Call this to accept a quest from available list
    public void AcceptQuest(Quest quest)
    {
        if (!activeQuests.Contains(quest) && availableQuests.Contains(quest))
        {
            activeQuests.Add(quest);
            OnQuestAccepted.Invoke(quest);
        }
    }

    // Call this when monster is killed
    public void OnKill(string tag)
    {
        UpdateObjectives(ObjectiveType.Kill, tag);
    }

    // Call this when item is collected
    public void OnCollect(string tag)
    {
        UpdateObjectives(ObjectiveType.Collect, tag);
    }

    private void UpdateObjectives(ObjectiveType type, string targetTag)
    {
        for (int i = activeQuests.Count - 1; i >= 0; i--)
        {
            var quest = activeQuests[i];
            bool changed = false;
            foreach (var obj in quest.objectives)
            {
                if (obj.type == type && obj.targetTag == targetTag && !obj.IsComplete())
                {
                    obj.currentAmount++;
                    changed = true;
                }
            }
            if (changed) OnQuestUpdated.Invoke(quest);
            if (quest.objectives.TrueForAll(o => o.IsComplete()))
            {
                CompleteQuest(quest);
            }
        }
    }

    private void CompleteQuest(Quest quest)
    {
        activeQuests.Remove(quest);
        // grant rewards
        var playerStats = Object.FindFirstObjectByType<PlayerCharacterStats>();
        if (playerStats) playerStats.GainXP(quest.xpReward);
        var inv = Object.FindFirstObjectByType<Inventory>();
        if (inv)
        {
            foreach (var item in quest.itemRewards)
                inv.AddItem(item);
        }
        OnQuestCompleted.Invoke(quest);
    }
}

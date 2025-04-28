using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewQuest", menuName = "RPG/Quest", order = 1)]
public class Quest : ScriptableObject
{
    public string questName;
    public string description;
    public List<QuestObjective> objectives;
    public int xpReward;
    public List<Item> itemRewards = new List<Item>();
}

using UnityEngine;

public enum ObjectiveType { Kill, Collect, Talk }

[System.Serializable]
public class QuestObjective
{
    public ObjectiveType type;
    public string description;
    public string targetTag; // e.g., "Monster" or "Item"
    public int requiredAmount;
    [HideInInspector] public int currentAmount;
    public bool IsComplete() => currentAmount >= requiredAmount;
}

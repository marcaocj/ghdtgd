using UnityEngine;
using UnityEngine.UI;

public class QuestSlot : MonoBehaviour
{
    public Text questNameText;
    public Text progressText;
    private Quest quest;
    private System.Action<Quest> onComplete;

    public void SetQuest(Quest q, System.Action<Quest> onCompleteCallback)
    {
        quest = q;
        onComplete = onCompleteCallback;
        questNameText.text = quest.questName;
        UpdateProgress();
    }

    public void UpdateProgress()
    {
        int done = 0;
        int total = 0;
        foreach (var obj in quest.objectives)
        {
            done += obj.currentAmount;
            total += obj.requiredAmount;
        }
        progressText.text = done + "/" + total;
        if (done >= total)
            progressText.color = Color.green;
        else
            progressText.color = Color.white;
    }

    public void OnClick()
    {
        // entrega se completo
        int done = 0, total = 0;
        foreach (var obj in quest.objectives) { done += obj.currentAmount; total += obj.requiredAmount; }
        if (done >= total && onComplete != null)
            onComplete.Invoke(quest);
    }
}

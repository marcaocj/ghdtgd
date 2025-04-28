using UnityEngine;
using UnityEngine.UI;
using Game.Stats;

/// <summary>
/// Manages UI for level, XP bar, and attribute point allocation.
/// </summary>
public class LevelManager : MonoBehaviour
{
    public CharacterStatsData stats;
    public Slider xpSlider;
    public Text levelText;
    public Text xpText;
    public Text pointsText;
    public Button btnSTR;
    public Button btnAGI;
    public Button btnVIT;
    public Button btnINT;
    public Button btnDEX;
    public Button btnLUK;

    void Start()
    {
        stats.OnLevelUp.AddListener(UpdateUI);
        btnSTR.onClick.AddListener(() => Allocate(AttributeType.STR));
        btnAGI.onClick.AddListener(() => Allocate(AttributeType.AGI));
        btnVIT.onClick.AddListener(() => Allocate(AttributeType.VIT));
        btnINT.onClick.AddListener(() => Allocate(AttributeType.INT));
        btnDEX.onClick.AddListener(() => Allocate(AttributeType.DEX));
        btnLUK.onClick.AddListener(() => Allocate(AttributeType.LUK));
        UpdateUI();
    }

    void Allocate(AttributeType type)
    {
        if (stats.AllocatePoint(type)) UpdateUI();
    }

    void UpdateUI()
    {
        if (xpSlider)
        {
            xpSlider.maxValue = stats.expToLevelUp;
            xpSlider.value = stats.currentXP;
        }
        if (levelText) levelText.text = "Level: " + stats.level;
        if (xpText) xpText.text = stats.currentXP + " / " + stats.expToLevelUp;
        if (pointsText) pointsText.text = "Points: " + stats.availablePoints;

        bool canAllocate = stats.availablePoints > 0;
        btnSTR.interactable = canAllocate;
        btnAGI.interactable = canAllocate;
        btnVIT.interactable = canAllocate;
        btnINT.interactable = canAllocate;
        btnDEX.interactable = canAllocate;
        btnLUK.interactable = canAllocate;
    }
}

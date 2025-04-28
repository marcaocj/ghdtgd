using UnityEngine;

public class Mana : MonoBehaviour
{
    public float maxMana;
    public float currentMana;

    public void Initialize(float mp)
    {
        maxMana = mp;
        currentMana = maxMana;
    }

    public void SetMaxMana(float mp)
    {
        maxMana = mp;
        currentMana = Mathf.Min(currentMana, maxMana);
    }

    public bool UseMana(float amount)
    {
        if (currentMana >= amount)
        {
            currentMana -= amount;
            return true;
        }
        return false;
    }
}

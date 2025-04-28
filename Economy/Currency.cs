using UnityEngine;

/// <summary>
/// Manages player's gold currency.
/// </summary>
public class Currency : MonoBehaviour
{
    public int gold = 0;

    /// <summary>
    /// Attempts to spend gold. Returns true if successful.
    /// </summary>
    public bool Spend(int amount)
    {
        if (gold >= amount)
        {
            gold -= amount;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Adds gold to the player.
    /// </summary>
    public void Earn(int amount)
    {
        gold += amount;
    }
}

using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    public delegate void OnDeathHandler();
    public event OnDeathHandler OnDeath;

    public void Initialize(float hp)
    {
        maxHealth = hp;
        currentHealth = maxHealth;
    }

    public void SetMaxHealth(float hp)
    {
        maxHealth = hp;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0f)
        {
            currentHealth = 0f;
            Die();
        }
    }

    private void Die()
    {
        OnDeath?.Invoke();
        // Only destroy non-player entities
        if (!CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}

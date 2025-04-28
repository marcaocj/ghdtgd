using UnityEngine;

/// <summary>
/// Destrói automaticamente itens caídos após um tempo.
/// </summary>
public class GroundItemLifetime : MonoBehaviour
{
    [Tooltip("Tempo antes do item desaparecer (segundos)")]
    public float lifetime = 60f;

    private float timer;

    void OnEnable()
    {
        timer = lifetime;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            Destroy(gameObject);
        }
    }
}

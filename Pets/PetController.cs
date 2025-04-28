using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Controls pet behavior: follow player and pick up items.
/// </summary>
public class PetController : MonoBehaviour
{
    [HideInInspector] public Transform player;
    [HideInInspector] public float followSpeed;
    [HideInInspector] public float followDistance;
    [HideInInspector] public float lootRange;

    private Inventory inventory;

    void Start()
    {
        inventory = Object.FindFirstObjectByType<Inventory>();
    }

    void Update()
    {
        if (player == null) return;
        // Follow logic
        float dist = Vector3.Distance(transform.position, player.position);
        if (dist > followDistance)
            transform.position = Vector3.MoveTowards(transform.position, player.position, followSpeed * Time.deltaTime);

        // Loot items
        Collider[] colliders = Physics.OverlapSphere(transform.position, lootRange);
        foreach (var col in colliders)
        {
            if (col.CompareTag("Item"))
            {
                var pickup = col.GetComponent<ItemPickup>();
                if (pickup != null && inventory != null && inventory.AddItem(pickup.item))
                    Destroy(col.gameObject);
            }
        }
    }
}

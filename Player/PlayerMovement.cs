using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public Camera cam;
    private NavMeshAgent agent;
    private PlayerAttack playerAttack;
    public Inventory inventory; // reference to player inventory
    [Header("Targeting")]
    public SkillManager skillManager;

    void Start()
    {
        if (cam == null) cam = Camera.main;
        agent = GetComponent<NavMeshAgent>();
        playerAttack = GetComponent<PlayerAttack>();
        inventory = GetComponent<Inventory>();
        if (skillManager == null)
        {
            skillManager = Object.FindFirstObjectByType<SkillManager>();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("Monster"))
                {
                    // select monster as current target for skills
                    if (skillManager != null)
                        skillManager.currentTarget = hit.collider.gameObject;
                    playerAttack.Attack(hit.collider.gameObject);
                }
                else if (hit.collider.CompareTag("Item"))
                {
                    // Pick up ground item into inventory
                    var pickup = hit.collider.GetComponent<ItemPickup>();
                    if (pickup != null && inventory != null)
                    {
                        if (inventory.AddItem(pickup.item))
                            Destroy(hit.collider.gameObject);
                        else
                            Debug.Log("Inventory full!");
                    }
                }
                else
                {
                    // move to point and clear target
                    agent.SetDestination(hit.point);
                    if (skillManager != null)
                        skillManager.currentTarget = null;
                }
            }
        }
    }
}

using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange = 2f;
    public float attackDamage = 10f;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
            Debug.LogError($"Animator component missing on {gameObject.name}");
    }

    public void Attack(GameObject target)
    {
        if (Vector3.Distance(transform.position, target.transform.position) <= attackRange)
        {
            if (animator != null)
                animator.SetTrigger("Attack");
            else
                Debug.LogWarning("Attack animation skipped: Animator missing.");
            // aplicar dano simples
            var health = target.GetComponent<Health>();
            if (health) health.TakeDamage(attackDamage);
        }
        else
        {
            // mover até o alvo antes de atacar
            var movement = GetComponent<UnityEngine.AI.NavMeshAgent>();
            movement.SetDestination(target.transform.position);
        }
    }
}

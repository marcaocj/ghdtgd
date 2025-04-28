using UnityEngine;
using UnityEngine.AI;
using Game.Monsters;
using Game.Player;

public class MonsterAI : MonoBehaviour
{
    public float detectRange = 10f;
    public float attackRange = 2f;
    public float attackDamage = 5f;
    public float patrolRadius = 5f;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;
    public float xpReward = 20f; // XP given to player on death

    private NavMeshAgent agent;
    private Animator animator;
    private Vector3 startPos;
    private GameObject player;
    private Health health;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        if (animator == null)
            Debug.LogError($"Animator component missing on {gameObject.name}");
        startPos = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
        health = GetComponent<Health>();
        health.OnDeath += OnDeath;
        StartPatrol();
    }

    void Update()
    {
        float distToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distToPlayer <= detectRange)
        {
            agent.speed = chaseSpeed;
            if (distToPlayer > attackRange)
                agent.SetDestination(player.transform.position);
            else
                AttackPlayer();
        }
        else
        {
            Patrol();
        }
    }

    void StartPatrol()
    {
        agent.speed = patrolSpeed;
        SetRandomPatrolTarget();
    }

    void Patrol()
    {
        if (!agent.hasPath || agent.remainingDistance < 0.5f)
            SetRandomPatrolTarget();
    }

    void SetRandomPatrolTarget()
    {
        Vector2 randPoint = Random.insideUnitCircle * patrolRadius;
        Vector3 target = startPos + new Vector3(randPoint.x, 0, randPoint.y);
        agent.SetDestination(target);
    }

    void AttackPlayer()
    {
        if (animator != null)
            animator.SetTrigger("Attack");
        else
            Debug.LogWarning($"Attack animation skipped: Animator missing on {gameObject.name}");
        player.GetComponent<Health>().TakeDamage(attackDamage);
    }

    void OnDeath()
    {
        // Award XP to player
        var playerStats = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerCharacterStats>();
        if (playerStats != null) playerStats.GainXP(xpReward);
        // Schedule respawn
        MobSpawner.Instance.ScheduleRespawn(startPos, transform.rotation, MobSpawner.Instance.respawnDelay);
    }
}

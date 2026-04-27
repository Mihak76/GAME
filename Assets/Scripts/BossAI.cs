using UnityEngine;
using UnityEngine.AI;

public class BossAI : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 15f;
    public float attackRange = 3f;
    public float attackCooldown = 2f;

    private NavMeshAgent agent;
    private float lastAttackTime;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        RaycastHit hit;
        Vector3 direction = (player.position - transform.position).normalized;

        // Detection (line of sight)
        if (Physics.Raycast(transform.position + Vector3.up * 1.5f, direction, out hit, detectionRange))
        {
            if (hit.transform == player)
            {
                agent.SetDestination(player.position);
            }
        }

        // Attack
        if (distance <= attackRange)
        {
            Attack();
        }
    }

    void Attack()
    {
        if (Time.time < lastAttackTime + attackCooldown) return;

        lastAttackTime = Time.time;

        Debug.Log("Boss attacks player!");

        // Tukaj lahko dodaš damage:
        // player.GetComponent<PlayerHealth>().TakeDamage(10);
    }
}
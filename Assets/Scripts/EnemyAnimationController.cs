using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimationController : MonoBehaviour
{
    public RangeDetector rangeDetector;
    public Animator animator;
    public NavMeshAgent agent;

    public float attackDistance = 2.5f;

    private GameObject target;

    void Start()
    {
        if (!agent) agent = GetComponent<NavMeshAgent>();
        if (!animator) animator = GetComponentInChildren<Animator>();
        if (!rangeDetector) rangeDetector = GetComponent<RangeDetector>();

        agent.stoppingDistance = attackDistance;
        agent.autoBraking = true;
    }

    void Update()
    {
        target = rangeDetector.UpdateDetector();

        if (target != null)
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);

            if (distance > attackDistance)
            {
                // Use NavMeshAgent for movement
                agent.enabled = true;
                agent.SetDestination(target.transform.position);

                animator.SetBool("isRunning", true);
                animator.SetBool("isAttacking", false);
            }
            else
            {
                // Stop NavMeshAgent, use Root Motion for attack
                agent.enabled = false;

                animator.SetBool("isRunning", false);
                animator.SetBool("isAttacking", true);
            }
        }
        else
        {
            agent.enabled = false;
            animator.SetBool("isRunning", false);
            animator.SetBool("isAttacking", false);
        }
    }
}
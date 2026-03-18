using UnityEngine;
using UnityEngine.AI;

public class EnemyRootMotionFinal : MonoBehaviour
{
    public RangeDetector rangeDetector;
    public Animator animator;
    public NavMeshAgent agent;
    public float attackDistance = 2.5f;

    private GameObject target;

    void Update()
    {
        // 1. Poišči target
        target = rangeDetector.UpdateDetector();

        if (target == null)
        {
            // Ni targeta → idle
            animator.SetBool("isRunning", false);
            animator.SetBool("isAttacking", false);
            agent.isStopped = true;
            return;
        }

        // 2. Razdalja do Playerja
        float distance = Vector3.Distance(transform.position, target.transform.position);

        // 3. Obrni Enemy proti Playerju
        Vector3 lookDir = target.transform.position - transform.position;
        lookDir.y = 0;
        if (lookDir != Vector3.zero)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDir), Time.deltaTime * 5f);

        // 4. Premik / animacije
        if (distance > attackDistance)
        {
            agent.isStopped = false;                // kapsula se premika
            agent.SetDestination(target.transform.position);

            animator.SetBool("isRunning", true);
            animator.SetBool("isAttacking", false);
        }
        else
        {
            agent.isStopped = true;                 // kapsula ustavi
            animator.SetBool("isRunning", false);
            animator.SetBool("isAttacking", true);
        }
    }
}
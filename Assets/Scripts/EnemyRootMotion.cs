using UnityEngine;

public class EnemySimple : MonoBehaviour
{
    public RangeDetector rangeDetector;
    public Animator animator;
    public float moveSpeed = 2.0f;
    public float attackDistance = 2.5f;

    private GameObject target;

    void Update()
    {
        target = rangeDetector.UpdateDetector();

        if (target != null)
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);

            if (distance > attackDistance)
            {
                // Move towards player
                animator.SetBool("isRunning", true);
                animator.SetBool("isAttacking", false);

                // premik kapsule/transforma
                Vector3 dir = (target.transform.position - transform.position).normalized;
                transform.position += dir * moveSpeed * Time.deltaTime;
                transform.LookAt(target.transform.position);
            }
            else
            {
                // Attack
                animator.SetBool("isRunning", false);
                animator.SetBool("isAttacking", true);
            }
        }
        else
        {
            // Idle
            animator.SetBool("isRunning", false);
            animator.SetBool("isAttacking", false);
        }
    }
}
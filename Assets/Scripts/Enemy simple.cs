using UnityEngine;

public class EnemyRootMotion : MonoBehaviour
{
    public RangeDetector rangeDetector;
    public Animator animator;
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
                // Run animacija bo premikala kapsulo
                animator.SetBool("isRunning", true);
                animator.SetBool("isAttacking", false);

                // obrni Enemy proti Playerju
                Vector3 lookDir = (target.transform.position - transform.position).normalized;
                lookDir.y = 0;
                if (lookDir != Vector3.zero)
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDir), Time.deltaTime * 5f);
            }
            else
            {
                // Attack animacija bo premikala kapsulo
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
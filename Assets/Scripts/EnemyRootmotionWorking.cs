using UnityEngine;

public class EnemyRootMotionWorking : MonoBehaviour
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

            // obrni Enemy proti Playerju
            Vector3 lookDir = target.transform.position - transform.position;
            lookDir.y = 0;
            if (lookDir != Vector3.zero)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDir), Time.deltaTime * 5f);

            if (distance > attackDistance)
            {
                // Run animacija premika Enemyja
                animator.SetBool("isRunning", true);
                animator.SetBool("isAttacking", false);
            }
            else
            {
                // Attack animacija premika Enemyja in ga ustavi
                animator.SetBool("isRunning", false);
                animator.SetBool("isAttacking", true);
            }
        }
        else
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isAttacking", false);
        }
    }
}
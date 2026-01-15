using UnityEngine;

public class LineOfSightDetector : MonoBehaviour
{
    [Header("Eye point")]
    [SerializeField] private Transform eyePoint;

    [Header("Settings")]
    [SerializeField] private float viewDistance = 15f;
    [SerializeField] private LayerMask obstacleMask;
    [SerializeField] private LayerMask targetMask;

    [SerializeField] private bool showDebug = true;

    public bool HasLineOfSight(GameObject target)
    {
        if (target == null || eyePoint == null) return false;

        Vector3 origin = eyePoint.position;
        Vector3 targetPos = target.transform.position + Vector3.up;
        Vector3 direction = targetPos - origin;
        float distance = direction.magnitude;
        direction.Normalize();

        if (Physics.Raycast(origin, direction, out RaycastHit hit, distance, obstacleMask | targetMask))
        {
            if (showDebug)
            {
                Debug.DrawLine(origin, hit.point,
                    hit.collider.gameObject == target ? Color.green : Color.red);
            }

            return hit.collider.gameObject == target;
        }

        return false;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (eyePoint != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(eyePoint.position, 0.15f);
        }
    }
#endif
}

using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AmmoManager.Instance.AddAmmo(15);
            Destroy(gameObject);
        }
    }
}
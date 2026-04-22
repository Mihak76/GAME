using UnityEngine;

public class DoorInteract : MonoBehaviour
{
    public float openAngle = 90f;
    public float speed = 3f;
    public bool isOpen = false;

    Quaternion closedRot;
    Quaternion openRot;

    public Transform player;
    public float interactDistance = 3f;

    void Start()
    {
        closedRot = transform.rotation;
        openRot = Quaternion.Euler(0, openAngle, 0) * closedRot;
    }

    void Update()
    {
        // preveri razdaljo do playerja
        if (Vector3.Distance(player.position, transform.position) > interactDistance)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            isOpen = !isOpen;
        }

        // smooth rotacija
        if (isOpen)
            transform.rotation = Quaternion.Slerp(transform.rotation, openRot, Time.deltaTime * speed);
        else
            transform.rotation = Quaternion.Slerp(transform.rotation, closedRot, Time.deltaTime * speed);
    }
}
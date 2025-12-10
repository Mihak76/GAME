using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 3f;
    Animator anim;
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float v = Input.GetAxis("Vertical");

        // Predvajaj animacijo, če se premikamo
        anim.SetFloat("Speed", Mathf.Abs(v));

        // Premikanje
        transform.Translate(Vector3.forward * v * speed * Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Unity Script | references
public class GameRespawn : MonoBehaviour
{
    public float threshold;

    // Unity Message | references
    void FixedUpdate()
    {
        if (transform.position.y < threshold)
        {
            transform.position = new Vector3(958.94f ,20.97f, 506.6f);
        }
    }
}
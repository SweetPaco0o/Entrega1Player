using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawnManager : MonoBehaviour
{
    public float threshold;


    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.y < threshold)
        {
            transform.position = new Vector3(4.5f, -4.6f, -0.9f);
        }
    }
}

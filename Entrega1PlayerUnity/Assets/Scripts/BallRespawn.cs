using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRespawn : MonoBehaviour
{
   public float threshold;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.y < threshold)
        {
              transform.position = new Vector3(-4.41f, 17.86f, 93.2f);  
        }
    }
}

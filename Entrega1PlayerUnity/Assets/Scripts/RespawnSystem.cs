using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnSystem : MonoBehaviour
{
    public float threshold;
    float level2 = 50;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.y < threshold)
        {
            if (transform.position.z < level2)
            {
              transform.position = new Vector3(4.22f, 14.63957f, -38.2f);  
            }
            else
            {
                transform.position = new Vector3(0.87f, 16.5f, 52f);
            }
        }
    }
}

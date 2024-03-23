using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoorControllerOpen : MonoBehaviour
{
    [SerializeField] private Animator myDoorL = null;
    [SerializeField] private Animator myDoorR = null;

    [SerializeField] private bool openTrigger = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (openTrigger)
            {
                myDoorL.Play("puertas_L", 0, 0.0f);
                gameObject.SetActive(false);
                myDoorR.Play("puertas_R", 0, 0.0f);
                gameObject.SetActive(false);
            }
        }
    }
}

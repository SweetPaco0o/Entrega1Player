using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoorControllerClose : MonoBehaviour
{
    [SerializeField] private Animator myDoorL = null;
    [SerializeField] private Animator myDoorR = null;

    [SerializeField] private bool closeTrigger = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (closeTrigger)
            {
                myDoorL.Play("puertas_L_close", 0, 0.0f);
                gameObject.SetActive(false);
                myDoorR.Play("puertas_R_close", 0, 0.0f);
                gameObject.SetActive(false);
            }
        }
    }
}

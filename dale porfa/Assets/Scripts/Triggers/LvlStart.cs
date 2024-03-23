using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlStart : MonoBehaviour
{
    public LevelMesaje LevelMesaje;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Activa el mensaje de nivel 1
            LevelMesaje.textActive();
        }
    }
}

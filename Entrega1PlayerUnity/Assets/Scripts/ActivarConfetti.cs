using UnityEngine;

public class ActivarConfetti : MonoBehaviour
{
    public GameObject confetiEffect; // Asigna el GameObject que contiene el efecto de confeti desde el editor de Unity

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Esfera")) // Comprueba si el objeto que activa el trigger es el jugador
        {
            ActivarEfectoConfeti();
        }
    }

    private void ActivarEfectoConfeti()
    {
        confetiEffect.SetActive(true); // Activa el efecto de confeti
        Invoke("DesactivarEfectoConfeti", 4f); // Desactiva el efecto de confeti después de 2 segundos
    }

    private void DesactivarEfectoConfeti()
    {
        confetiEffect.SetActive(false); // Desactiva el efecto de confeti
    }
}

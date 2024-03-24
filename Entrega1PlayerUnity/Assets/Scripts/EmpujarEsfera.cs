using UnityEngine;

public class EmpujarEsfera : MonoBehaviour
{
    public float fuerzaEmpuje = 5f; // Ajusta según sea necesario

    // Este método se llama cuando otro collider entra en el trigger de este objeto
    void OnTriggerEnter(Collider other)
    {
        // Verifica si el otro collider es la esfera
        if (other.CompareTag("Esfera"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Calcula la dirección desde el jugador hacia la esfera
                Vector3 direccionEmpuje = other.transform.position - transform.position;
                // Aplica una fuerza en esa dirección
                rb.AddForce(direccionEmpuje.normalized * fuerzaEmpuje, ForceMode.Impulse);
            }
        }
    }
}

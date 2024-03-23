using System.Collections;
using UnityEngine;

public class spawn : MonoBehaviour
{
    public GameObject woodPrefab; 
    public float time = 3f;
    public float lifetime = 11f;

    void Start()
    {
      
        StartCoroutine(SpawnWood());
    }

    IEnumerator SpawnWood()
    {
        while (true)
        {

            yield return new WaitForSeconds(time);


            GameObject newWood = Instantiate(woodPrefab, transform.position, Quaternion.Euler(90f, 90f, 0f));

            Destroy(newWood, lifetime);
        }
    }
}
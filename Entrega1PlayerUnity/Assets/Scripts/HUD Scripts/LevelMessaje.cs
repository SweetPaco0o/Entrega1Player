using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMesaje : MonoBehaviour
{
    public Text text;
    void Start()
    {
        text.enabled = false;
    }

    public void textActive()
    {
        text.enabled = true;
        StartCoroutine(textDeactivate());
    }

    IEnumerator textDeactivate()
    {
        yield return new WaitForSeconds(3f);
        text.enabled = false;
    }
}

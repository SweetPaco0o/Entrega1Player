using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crono : MonoBehaviour
{
    public Text TimeText;
    private float time = 0f;

    private int MinutesTime, SecondsTime, TenthsTime;
    private bool isRunning = false;

    void Chronometer()
    {
        if (isRunning)
        {
            time += Time.deltaTime;

            MinutesTime = Mathf.FloorToInt(time / 60);
            SecondsTime = Mathf.FloorToInt(time % 60);
            TenthsTime = Mathf.FloorToInt((time % 1) * 100);

            TimeText.text = string.Format("{0:00}:{1:00}:{2:00}", MinutesTime, SecondsTime, TenthsTime);
        }
    }

    void Update()
    {
        Chronometer();
    }

    public void StartChronometer()
    {
        isRunning = true;
    }

    public void StopChronometer()
    {
        isRunning = false;
    }
}

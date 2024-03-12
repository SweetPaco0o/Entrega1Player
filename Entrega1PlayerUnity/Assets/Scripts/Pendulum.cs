using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    Quaternion _start, _end;

    [SerializeField]
    private float _angle = 0.0f;

    [SerializeField]
    private float _speed = 2.0f;

    [SerializeField]
    private float _startTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        _start = PendulumRotation(_angle);
        _end = PendulumRotation(-_angle);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _startTime += Time.deltaTime;
        transform.rotation = Quaternion.Lerp(_start, _end, (Mathf.Sin(_startTime * _speed + Mathf.PI / 2) + 1.0f) / 2.0f);
    }

    void ResetTimer()
    {
        _startTime = 0.0f;
    }
    Quaternion PendulumRotation(float angle)
    {
        var pendulumRotation = transform.rotation;
        var angleY = pendulumRotation.eulerAngles.y + angle;

        if(angleY > 180)
        {
            angleY -= 360;
        }
        else if(angleY < -180)
        {
            angleY += 360;
        }

        pendulumRotation.eulerAngles = new Vector3(angleY, 0, 0);
        return pendulumRotation;
    }
}

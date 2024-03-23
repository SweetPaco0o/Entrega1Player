using UnityEngine;

public class Spring_Line : MonoBehaviour
{
    LineRenderer line;

    public Transform StartPoint;
    public Transform EndPoint;
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        SetPoints();
    }

    private void SetPoints()
    {
        line.SetPosition(0, StartPoint.position);
        line.SetPosition(1, EndPoint.position);
    }
}
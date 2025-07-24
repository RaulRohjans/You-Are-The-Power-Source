using UnityEngine;

public class PowerLine : MonoBehaviour
{
    public Transform start;

    public Transform end;
    private LineRenderer lr;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (start != null && end != null)
        {
            lr.SetPosition(0, start.position);
            lr.SetPosition(1, end.position);
        }
    }
}

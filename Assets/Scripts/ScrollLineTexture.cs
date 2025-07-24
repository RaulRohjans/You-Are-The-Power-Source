using UnityEngine;

public class ScrollLineTexture : MonoBehaviour
{
    [SerializeField]
    private float scrollSpeed = 2f;
    private LineRenderer lineRenderer;
    private Material lineMat;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineMat = lineRenderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        if (lineMat != null)
        {
            float offset = Time.time * scrollSpeed;
            lineMat.mainTextureOffset = new Vector2(offset, 0);
        }
    }
}

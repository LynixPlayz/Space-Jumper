using UnityEngine;

public class DashedLineRenderer : MonoBehaviour
{
    public float dashLength = 0.2f;     // Length of each dash
    public float gapLength = 0.1f;      // Length of each gap
    public float speed = 1f;            // Adjust this value to control line movement speed

    private LineRenderer lineRenderer;
    private float currentOffset;        // Current offset for the dashed line

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;
    }

    private void Update()
    {
        currentOffset += Time.deltaTime * speed;

        if (currentOffset > dashLength + gapLength)
        {
            currentOffset -= dashLength + gapLength;
        }

        UpdateDashedLine();
    }

    private void UpdateDashedLine()
    {
        int vertexCount = (int)(lineRenderer.GetPosition(1).x / (dashLength + gapLength)) * 2;
        lineRenderer.positionCount = vertexCount;

        for (int i = 0; i < vertexCount; i++)
        {
            float t = (float)i / (vertexCount - 1);
            float x = t * lineRenderer.GetPosition(1).x;

            lineRenderer.SetPosition(i, new Vector3(x, 0f, 0f));
        }

        float textureTile = lineRenderer.GetPosition(1).x / (dashLength + gapLength);
        lineRenderer.material.mainTextureScale = new Vector2(textureTile, 1f);

        float textureOffset = currentOffset / (dashLength + gapLength);
        lineRenderer.material.mainTextureOffset = new Vector2(-textureOffset, 0f);
    }
}

using UnityEngine;

public class MoveDashedLine : MonoBehaviour {
    public float speed = 1f;          // Adjust this value to control line movement speed
    public float dashSize = 0.2f;    // Adjust this value to control the size of each dash

    private LineRenderer lineRenderer;
    private float textureOffset = 0f;

    private void Start() {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update() {
        // Calculate the texture offset based on time and speed
        textureOffset += Time.deltaTime * speed;
        float tiling = 1f / dashSize;

        lineRenderer.sharedMaterial.mainTextureOffset = new Vector2(textureOffset, 0f);
        lineRenderer.sharedMaterial.mainTextureScale = new Vector2(tiling, 1f);
    }
}

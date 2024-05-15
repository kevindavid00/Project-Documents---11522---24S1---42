using UnityEngine;

public class PlaneYMover : MonoBehaviour
{
    [SerializeField] private float startY = -1f; // Starting y position
    [SerializeField] private float endY = -1f; // Ending y position
    [SerializeField] private float increment = 0.1f; // Increment value per second
    [SerializeField] private float speedMultiplier = 2f; // Adjust speed (optional)

    private float currentY;
    private MeshRenderer meshRenderer; // Reference to the plane's mesh renderer

    void Start()
    {
        currentY = startY;
        meshRenderer = GetComponent<MeshRenderer>(); // Get the mesh renderer component
    }

    void Update()
    {
        currentY += increment * speedMultiplier * Time.deltaTime;

        // Clamp the y position to the defined range
        currentY = Mathf.Clamp(currentY, startY, endY);

        // Update the plane's position
        transform.position = new Vector3(transform.position.x, currentY, transform.position.z);
        
    }
}

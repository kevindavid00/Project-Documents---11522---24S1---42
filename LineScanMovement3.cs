using UnityEngine;

public class LineScanMovement3 : MonoBehaviour
{    // zone 2 
    [SerializeField] private Transform objectToMove; // Reference to the object to be moved
    [SerializeField] private float movementSpeed; // Speed of the object's movement
    [SerializeField] private float scanResolution; // Optional: Distance between scan points (not used in this update)

    private float startX = 125.7f; // Minimum X coordinate
    private float endX = 260f; // Maximum X coordinate
    private float minZ = 120f; // Minimum Z coordinate
    private float maxZ = 240f; // Maximum Z coordinate

    private bool moveXPositive = true; // Flag to indicate positive or negative X movement
    private float currentX;
    private float currentZ;
    private float zIncrement = 15.0f; // Amount to increment Z when reaching a boundary (adjust as needed)

    private void Start()
    {
        currentX = startX;
        currentZ = minZ;
    }

    DroneMovementScript droneScript;

    private void Awake()
    {
        droneScript = FindObjectOfType<DroneMovementScript>();
    }

    private void Update()
    {

        // Move the drone in the X direction
        currentX += moveXPositive ? movementSpeed * Time.deltaTime : -movementSpeed * Time.deltaTime;

        // Check if the drone has reached the end of the X boundary
        if (currentX > endX || currentX < startX)
        {

            moveXPositive = !moveXPositive;

            // Increment Z when reaching a boundary
            currentZ += zIncrement;

            // Clamp the Z position to the terrain boundaries
            currentZ = Mathf.Clamp(currentZ, minZ, maxZ);
        }
        if (currentZ == 225f && currentX < 195.6179 && currentX > 193)
        {

             droneScript.HandleErrorMessage("Zone 3 has 4 survivors located at (295.7,0,189.7)");
        }
        // Move the drone in the Z direction (not used in this update)
        // currentZ += moveXPositive ? 0 : movementSpeed * Time.deltaTime; 

        // Clamp the X position to the terrain boundaries
        currentX = Mathf.Clamp(currentX, startX, endX);

        // Perform your scan logic at the current position
        // This could involve raycasting, checking for colliders, etc.

        objectToMove.position = new Vector3(currentX, objectToMove.position.y, currentZ);

    }
}

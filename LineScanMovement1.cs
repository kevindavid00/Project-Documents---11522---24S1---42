using UnityEditor.UI;
using UnityEngine;
using System.Linq;

public class LineScanMovement1 : MonoBehaviour
{    // zone 1 
    [SerializeField] private Transform objectToMove; // Reference to the object to be moved
    [SerializeField] private float movementSpeed; // Speed of the object's movement
    [SerializeField] private float scanResolution; // Optional: Distance between scan points (not used in this update)

    private float startX = -30.7f; // Minimum X coordinate
    private float endX = 120.4f; // Maximum X coordinate
    private float minZ = 10.5f; // Minimum Z coordinate
    private float maxZ = 110f; // Maximum Z coordinate

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
        
        if (currentZ == 55.5f && currentX < 23.5 && currentX > 21.5)
        {
            droneScript.HandleErrorMessage("Zone 1 has 8 survivors located at (113.6,0,21.5)");
        }
        /** if (currentX > 95 && currentX < 98)
        {
            droneScript.HandleErrorMessage("hellox)");
        }**/
        // Move the drone in the Z direction (not used in this update)
        // currentZ += moveXPositive ? 0 : movementSpeed * Time.deltaTime; 

        // Clamp the X position to the terrain boundaries
        currentX = Mathf.Clamp(currentX, startX, endX);

        // Perform your scan logic at the current position
        // This could involve raycasting, checking for colliders, etc.

        objectToMove.position = new Vector3(currentX, objectToMove.position.y, currentZ);
        
    }
}

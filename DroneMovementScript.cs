using UnityEngine;

public interface InterfaceMessageHandler
{
    void HandleErrorMessage(string errorMessage);
}

public class DroneMovementScript : MonoBehaviour, InterfaceMessageHandler
{
    [SerializeField] private float movementForwardSpeed = 5000.0f;
    [SerializeField] private float rotationSpeed = 2.5f;
    [SerializeField] private float lerpSpeed = 10f; // Adjust for desired smoothness
    [SerializeField] private float tiltAmountForward = 0.0f;
    [SerializeField] private float strafeSpeed = 5000.0f; // Adjust for desired strafe speed

    private Rigidbody ourDrone;
    private float wantedYRotation;

    void Awake()
    {
        ourDrone = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        MoveUpDown();
        MoveForward();
        Rotate();
        MoveLeftRight();
    }

    void MoveUpDown()
    {
        float upForce = 98.1f;
        if (Input.GetKey(KeyCode.I))
        {
            upForce = 8500.0f;
        }
        else if (Input.GetKey(KeyCode.K))
        {
            upForce = -8000.0f;
        }
        ourDrone.velocity = new Vector3(ourDrone.velocity.x, ourDrone.velocity.y, 0.0f);
        ourDrone.AddRelativeForce(Vector3.up * upForce);
    }

    void MoveForward()
    {
        float targetSpeed = Input.GetAxis("Vertical") * movementForwardSpeed;
        ourDrone.velocity = Vector3.ClampMagnitude(Vector3.forward * Mathf.Lerp(ourDrone.velocity.z, targetSpeed, lerpSpeed * Time.deltaTime), movementForwardSpeed);
    }

    void Rotate()
    {
        float currentYRotation = 0.0f;
        float rotationYVelocity = 0.0f;

        if (Input.GetKey(KeyCode.J))
        {
            wantedYRotation -= rotationSpeed;
        }
        if (Input.GetKey(KeyCode.L))
        {
            wantedYRotation += rotationSpeed;
        }

        currentYRotation = Mathf.SmoothDamp(currentYRotation, wantedYRotation, ref rotationYVelocity, 0.05f);
        ourDrone.rotation = Quaternion.Euler(new Vector3(tiltAmountForward, currentYRotation, ourDrone.rotation.z));
    }

    // New function for left-right movement
    void MoveLeftRight()
    {
        float strafeForce = strafeSpeed * Input.GetAxis("Horizontal");
        ourDrone.AddForce(transform.right * strafeForce);
    }

    public void HandleErrorMessage(string errorMessage)
    {
        Debug.Log(errorMessage);
    }
}
 
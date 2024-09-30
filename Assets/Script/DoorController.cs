using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Transform leftDoor;
    public Transform rightDoor;
    public float doorOpenAngle = 90f;  // The angle the door will open
    public float openSpeed = 5f;       // The speed of opening
    public bool isOpen = false;
    
    private Quaternion leftClosedRotation;
    private Quaternion rightClosedRotation;
    private Quaternion leftOpenRotation;
    private Quaternion rightOpenRotation;

    void Start()
    {
        // Store the initial rotations (closed positions)
        leftClosedRotation = leftDoor.localRotation;
        rightClosedRotation = rightDoor.localRotation;

        // Calculate the target open rotations
        leftOpenRotation = leftClosedRotation * Quaternion.Euler(0f, doorOpenAngle, 0f);
        rightOpenRotation = rightClosedRotation * Quaternion.Euler(0f, -doorOpenAngle, 0f);
    }

    void Update()
    {
        // If the door is opening, smoothly rotate to the open position
        if (isOpen)
        {
            leftDoor.localRotation = Quaternion.Slerp(leftDoor.localRotation, leftOpenRotation, Time.deltaTime * openSpeed);
            rightDoor.localRotation = Quaternion.Slerp(rightDoor.localRotation, rightOpenRotation, Time.deltaTime * openSpeed);
        }
        else
        {
            leftDoor.localRotation = Quaternion.Slerp(leftDoor.localRotation, leftClosedRotation, Time.deltaTime * openSpeed);
            rightDoor.localRotation = Quaternion.Slerp(rightDoor.localRotation, rightClosedRotation, Time.deltaTime * openSpeed);
        }
    }

    // Trigger to detect when player enters
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // Check if the object has the "Player" tag
        {
            isOpen = true;  // Open the doors
        }
    }

    // Trigger to detect when player exits
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))  // Check if the object has the "Player" tag
        {
            isOpen = false;  // Close the doors
        }
    }
}

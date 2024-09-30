using UnityEngine;

public class SlidingGate : MonoBehaviour
{
    public Transform leftGate;
    public Transform rightGate;

    public Vector3 leftClosedPosition;
    public Vector3 leftOpenPosition;
    public Vector3 rightClosedPosition;
    public Vector3 rightOpenPosition;

    public float speed = 1.0f;
    public bool isPlayerInZone = false;

    private void Start()
    {
        // Set the gate's initial positions
        leftClosedPosition = leftGate.localPosition;
        rightClosedPosition = rightGate.localPosition;

        leftOpenPosition = leftClosedPosition + new Vector3(0, 0, 30f); // Adjust values as needed
        rightOpenPosition = rightClosedPosition + new Vector3(0, 0, 30f); // Adjust values as needed
    }

    private void Update()
    {
        if (isPlayerInZone)
        {
            // Slide gates open
            leftGate.localPosition = Vector3.Lerp(leftGate.localPosition, leftOpenPosition, Time.deltaTime * speed);
            rightGate.localPosition = Vector3.Lerp(rightGate.localPosition, rightOpenPosition, Time.deltaTime * speed);
        }
        else
        {
            // Slide gates closed
            leftGate.localPosition = Vector3.Lerp(leftGate.localPosition, leftClosedPosition, Time.deltaTime * speed);
            rightGate.localPosition = Vector3.Lerp(rightGate.localPosition, rightClosedPosition, Time.deltaTime * speed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInZone = false;
        }
    }
}

using UnityEngine;

public class SwitchControl : MonoBehaviour
{
    public Light[] ceilingLights; // Array of Light objects
    public Transform[] ceilingFans; // Reference to the Ceiling Fan object
    public float[] fanSpeeds = { 0f, 200f, 400f, 600f }; // Speed stages: 0 (off), 1, 2, and 3
    private int currentFanStage = 0; // Current fan speed stage (initially off)

    public bool isPlayerInZone = false; // Check if player is in the trigger zone
    public bool areLightsOn = false; // Initial state of the lights

    private void Start()
    {
        // Set the initial state of all lights
        SetLightsState(areLightsOn);
    }

    private void Update()
    {
        // Check if the player is in the trigger zone
        if (isPlayerInZone)
        {
            // Turn lights on/off with left mouse click
            if (Input.GetMouseButtonDown(0)) // Left mouse button (0)
            {
                areLightsOn = !areLightsOn;
                SetLightsState(areLightsOn);
            }

            // Change fan speed with right mouse click
            if (Input.GetMouseButtonDown(1)) // Right mouse button (1)
            {
                currentFanStage++;
                if (currentFanStage > 3) currentFanStage = 0; // Cycle back to 0 after reaching stage 3
            }
        }

        // Rotate the ceiling fan based on the current speed stage
        if (currentFanStage > 0)
        {
            foreach (Transform fan in ceilingFans)
            {
                fan.Rotate(fanSpeeds[currentFanStage] * Time.deltaTime * Vector3.up, Space.Self);
            }
        }
    }

    // Method to set the state of all lights
    private void SetLightsState(bool state)
    {
        foreach (Light light in ceilingLights)
        {
            light.enabled = state;
        }
    }

    // When the player enters the trigger zone
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Assuming the player GameObject is tagged as "Player"
        {
            isPlayerInZone = true;
        }
    }

    // When the player exits the trigger zone
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInZone = false;
        }
    }
}

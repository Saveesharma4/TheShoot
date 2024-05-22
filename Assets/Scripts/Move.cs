using UnityEngine;

public class Hover : MonoBehaviour
{
    // Variables to control hover behavior
    public float hoverHeight = 0.5f; // Maximum height the object will hover
    public float hoverSpeed = 2f; // Speed of the hover

    // Reference to the camera
    public Transform cameraTransform;

    private Vector3 initialLocalPosition; // Initial local position of the object

    void Start()
    {
        // Store the initial local position of the object relative to the camera
        initialLocalPosition = transform.localPosition;
    }

    void Update()
    {
        // Calculate the new Y position using a sine wave
        float newY = Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;

        // Apply the new local position to the object relative to the camera
        transform.localPosition = new Vector3(initialLocalPosition.x, initialLocalPosition.y + newY, initialLocalPosition.z);
        
        // Keep the object following the camera's position
        transform.position = cameraTransform.position + transform.localPosition;
    }
}

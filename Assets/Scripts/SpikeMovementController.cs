using UnityEngine;

public class SpikeMovementController : MonoBehaviour
{
    public float moveSpeed = 10f;           // Speed of movement
    public float smoothSpeed = 10f;         // Speed of smoothing when moving back
    public Vector2 xBounds = new Vector2(-10f, 10f); // Movement limits on X-axis
    public Vector2 yBounds = new Vector2(-10f, 10f); // Movement limits on Y-axis
    public Color currentSpikeColor = Color.red; // Initial color of the spike

    private Vector3 targetPosition;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;  // Set the starting position (center)
        targetPosition = startPosition;      // Initialize the target position to the center
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        // Only update the target position if there is input
        if (moveX != 0 || moveY != 0)
        {
            targetPosition += new Vector3(moveX, moveY, 0) * moveSpeed * Time.deltaTime;
        }
        else
        {
            // Smoothly return to the center when no input is given
            targetPosition = startPosition;
        }

        // Clamp the target position within the bounds
        targetPosition.x = Mathf.Clamp(targetPosition.x, xBounds.x, xBounds.y);
        targetPosition.y = Mathf.Clamp(targetPosition.y, yBounds.x, yBounds.y);

        // Smoothly move the spike towards the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}
using System;
using UnityEngine;

public class SpikeController : MonoBehaviour
{
    public float moveSpeed = 10f;           // Speed of movement
    public float smoothSpeed = 10f;         // Speed of smoothing when moving back
    public Vector2 xBounds = new Vector2(-10f, 10f); // Movement limits on X-axis
    public Vector2 yBounds = new Vector2(-10f, 10f); // Movement limits on Y-axis
    public BubbleColor currentSpikeColor; // Initial color of the spike

    private Vector3 targetPosition;
    private Vector3 startPosition;
    private Material _spikeMaterial;
    void Start()
    {
        SetColor(currentSpikeColor);
        startPosition = transform.position;  // Set the starting position (center)
        targetPosition = startPosition;      // Initialize the target position to the center
    }
    public void SetColor(BubbleColor bubbleColor)
    {
        _spikeMaterial = GetComponent<Renderer>().material;
        if (_spikeMaterial != null)
        {
            _spikeMaterial.color = bubbleColor.ToUnityColor();
        }
    }
    
    private float moveX;
    private float moveY;
    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");
        MoveSpike();
    }
    
   void  MoveSpike()
    {
        if (moveX != 0 || moveY != 0)
        {
            targetPosition += new Vector3(moveX, moveY, 0) * moveSpeed * Time.deltaTime;
        }
        else
        {
            targetPosition = startPosition;
        }
        targetPosition.x = Mathf.Clamp(targetPosition.x, xBounds.x, xBounds.y);
        targetPosition.y = Mathf.Clamp(targetPosition.y, yBounds.x, yBounds.y);
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        ICollidable collidable = other.gameObject.GetComponent<ICollidable>();
        if (collidable != null)
        {
            collidable.OnCollide(currentSpikeColor);
        }
    }
    
    public void ChangeSpikeColor(BubbleColor newColor)
    {
        currentSpikeColor = newColor;
        UpdateSpikeColor();
    }

    private void UpdateSpikeColor()
    {
        if (_spikeMaterial != null)
        {
            _spikeMaterial.color = currentSpikeColor.ToUnityColor();
        }
    }
}
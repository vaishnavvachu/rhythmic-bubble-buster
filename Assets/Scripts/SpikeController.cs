using System;
using System.Collections.Generic;
using UnityEngine;

public class SpikeController : MonoBehaviour
{
    public float moveSpeed = 10f;           // Speed of movement
    public float smoothSpeed = 10f;         // Speed of smoothing when moving back
    public Vector2 xBounds = new Vector2(-10f, 10f); // Movement limits on X-axis
    public Vector2 yBounds = new Vector2(-10f, 10f); // Movement limits on Y-axis
    public BubbleColor currentSpikeColor; // Initial color of the spike

    private List<BubbleColor> collectedColors = new List<BubbleColor>();
    private int currentColorIndex = 0;
    private Vector3 targetPosition;
    private Vector3 startPosition;
    private Material _spikeMaterial;
    public TutorialController tutorialController;
    void Start()
    {
        SetColor(currentSpikeColor);
        startPosition = transform.position;  // Set the starting position (center)
        targetPosition = startPosition; 
        
        collectedColors.Add(currentSpikeColor);// Initialize the target position to the center
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
        HandleColorSwitch();
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
        
        if (tutorialController != null)
        {
            tutorialController.OnBubblePopped(); // Call this to update the tutorial
        }
    }
    private void HandleColorSwitch()
    {
        // Press a button (e.g., Space) to cycle through colors
        if (Input.GetKeyDown(KeyCode.Space) && collectedColors.Count > 1)
        {
            currentColorIndex = (currentColorIndex + 1) % collectedColors.Count;
            currentSpikeColor = collectedColors[currentColorIndex];

            Debug.Log($"Switched to color: {currentSpikeColor}");
            UpdateSpikeColor();
        }
    }
    public void CollectColor(BubbleColor color)
    {
        if (!collectedColors.Contains(color))
        {
            collectedColors.Add(color);
            Debug.Log($"Collected new color: {color}");
        }
        if (tutorialController != null)
        {
            tutorialController.OnBubblePopped(); // Call this to update the tutorial
        }
    }
  

    private void UpdateSpikeColor()
    {
        if (_spikeMaterial != null)
        {
            _spikeMaterial.color = currentSpikeColor.ToUnityColor();
        }
    }
}
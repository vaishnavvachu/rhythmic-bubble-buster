using UnityEngine;

public class Bubble : MonoBehaviour
{
    public float moveSpeed = 5f; 
    public Color bubbleColor; 
    public Vector3 direction;

    void Start()
    {
        direction = new Vector3(0, 0, -1);
    }

    void Update()
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime);
        if (transform.position.z <= 0.1f)
        {
            CheckCollision();
        }
    }

    private void CheckCollision()
    {
        GameObject spike = GameObject.FindWithTag("Spike");
        SpikeMovementController spikeController = spike.GetComponent<SpikeMovementController>();

        if (spikeController != null && spikeController.currentSpikeColor == bubbleColor)
        {
            Destroy(gameObject); // Pop the bubble if color matches
            Debug.Log("Bubble popped!");
        }
        else
        {
            //Debug.Log("Missed or wrong color!");
        }
    }

    public void SetColor(Color color)
    {
        bubbleColor = color;
        GetComponent<Renderer>().material.color = color;
    }
}
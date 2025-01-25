using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    public GameObject bubblePrefab;
    public Transform[] spawnPoints; // Fixed spawn positions
    //public Color[] bubbleColors;

    public void Start()
    {
        InvokeRepeating("SpawnBubbles", 0, 5f);
        //SpawnBubbles();
    }

    public void SpawnBubbles()
    {
        // Randomly spawn two bubbles at distinct points
        int index1 = Random.Range(0, spawnPoints.Length);
        int index2;
        do
        {
            index2 = Random.Range(0, spawnPoints.Length);
        } while (index2 == index1); // Ensure unique spawn points

        // Set bubble colors and spawn them at the positions
        SpawnBubbleAtPosition(spawnPoints[index1].position, BubbleColor.Red); // Red
        SpawnBubbleAtPosition(spawnPoints[index2].position, BubbleColor.Blue); // Blue
    }

    private void SpawnBubbleAtPosition(Vector3 position, BubbleColor color)
    {
        GameObject bubble = Instantiate(bubblePrefab, position, Quaternion.identity);
        bubble.GetComponent<Bubble>().SetColor(color);
    }
}
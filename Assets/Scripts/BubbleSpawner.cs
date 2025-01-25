using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    public GameObject bubblePrefab;
    public GameObject powerUpPrefab; // Prefab for the power-up
    public Transform[] spawnPoints; // Fixed spawn positions
    public float powerUpSpawnChance = 0.2f; // 20% chance to spawn a power-up

    public void Start()
    {
        InvokeRepeating("SpawnBubbles", 0, 5f);
    }

    public void SpawnBubbles()
    {
        // Randomly select two distinct spawn points
        int index1 = Random.Range(0, spawnPoints.Length);
        int index2;
        do
        {
            index2 = Random.Range(0, spawnPoints.Length);
        } while (index2 == index1);

        // Determine whether to spawn bubbles or power-ups
        SpawnObjectAtPosition(spawnPoints[index1].position);
        SpawnObjectAtPosition(spawnPoints[index2].position);
    }

    private void SpawnObjectAtPosition(Vector3 position)
    {
        if (Random.value < powerUpSpawnChance) // Spawn a power-up
        {
            Instantiate(powerUpPrefab, position, Quaternion.identity);
        }
        else // Spawn a bubble
        {
            GameObject bubble = Instantiate(bubblePrefab, position, Quaternion.identity);
            BubbleColor randomColor = (BubbleColor)Random.Range(0, System.Enum.GetValues(typeof(BubbleColor)).Length);
            bubble.GetComponent<Bubble>().SetColor(randomColor);
        }
    }
}
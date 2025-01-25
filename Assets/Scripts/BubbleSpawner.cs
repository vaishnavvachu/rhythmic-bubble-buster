using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    public Transform[] spawnPoints; // Fixed spawn positions
    public string bubbleTag = "Bubble"; // Tag for bubbles
    public string powerUpTag = "PowerUp"; // Tag for power-ups
    public float powerUpSpawnChance = 0.2f; // Chance to spawn a power-up

    private PoolManager _poolManager;

    void Start()
    {
        _poolManager = FindFirstObjectByType<PoolManager>();   //<PoolManager>(); // Find the PoolManager in the scene
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

        // Spawn objects at selected positions
        SpawnObjectAtPosition(spawnPoints[index1].position);
        SpawnObjectAtPosition(spawnPoints[index2].position);
    }

    private void SpawnObjectAtPosition(Vector3 position)
    {
        if (Random.value < powerUpSpawnChance) // Spawn a power-up
        {
            _poolManager.SpawnFromPool(powerUpTag, position, Quaternion.identity);
        }
        else // Spawn a bubble
        {
            GameObject bubble = _poolManager.SpawnFromPool(bubbleTag, position, Quaternion.identity);
            BubbleColor randomColor = (BubbleColor)Random.Range(0, System.Enum.GetValues(typeof(BubbleColor)).Length);
            bubble.GetComponent<Bubble>().SetColor(randomColor);
        }
    }
}
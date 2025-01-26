using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    public Transform[] spawnPoints; // Fixed spawn positions
    public string bubbleTag = "Bubble"; // Tag for bubbles
    public string powerUpTag = "PowerUp"; // Tag for power-ups

    public int level = 1; // Current level
    public float powerUpSpawnChance = 0.2f; // Chance to spawn a power-up

    private PoolManager _poolManager;
    private int bubbleSpawnCount = 0; // Tracks how many bubbles have been spawned
    private int powerUpSpawnCount = 0; // Tracks power-ups spawned in a level

    private int maxRedBubblesLevel1 = 10; // Max red bubbles in level 1 before power-up spawns
    private int maxBluePowerUpsLevel1 = 3; // Max blue power-ups in level 1
    private int maxColorsInLevel2 = 3; // Number of colors in level 2

    private void OnEnable()
    {
        BeatDetector.BeatEvent += SpawnOnBeat; // Subscribe to the beat event
    }

    private void OnDisable()
    {
        BeatDetector.BeatEvent -= SpawnOnBeat; // Unsubscribe to the beat event
    }

    void Start()
    {
        _poolManager = FindFirstObjectByType<PoolManager>();
    }

    public void SpawnBubbles()
    {
        int index = Random.Range(0, spawnPoints.Length);

        switch (level)
        {
            case 1:
                HandleLevel1Spawning(index);
                break;

            case 2:
                HandleLevel2Spawning(index);
                break;

            // Add cases for additional levels as needed
        }
    }

    private void HandleLevel1Spawning(int index)
    {
        if (bubbleSpawnCount < maxRedBubblesLevel1)
        {
            // Spawn red bubbles until maxRedBubblesLevel1 is reached
            SpawnBubbleAtPosition(spawnPoints[index].position, BubbleColor.Red);
            bubbleSpawnCount++;
        }
        else if (powerUpSpawnCount < maxBluePowerUpsLevel1)
        {
            // Spawn blue power-ups until maxBluePowerUpsLevel1 is reached
            SpawnPowerUpAtPosition(spawnPoints[index].position, BubbleColor.Blue);
            powerUpSpawnCount++;
        }
        else
        {
            // After red bubbles and blue power-ups, spawn red and blue bubbles randomly
            BubbleColor randomColor = (BubbleColor)Random.Range(0, 2); // 0 = Red, 1 = Blue
            SpawnBubbleAtPosition(spawnPoints[index].position, randomColor);
        }
    }


    private void HandleLevel2Spawning(int index)
    {
        // Spawn bubbles with red, blue, or a third color (e.g., yellow)
        BubbleColor randomColor = (BubbleColor)Random.Range(0, maxColorsInLevel2);
        SpawnBubbleAtPosition(spawnPoints[index].position, randomColor);
    }

    private void SpawnBubbleAtPosition(Vector3 position, BubbleColor color)
    {
        GameObject bubble = _poolManager.SpawnFromPool(bubbleTag, position, Quaternion.identity);
        bubble.GetComponent<Bubble>().SetColor(color);
    }

    private void SpawnPowerUpAtPosition(Vector3 position, BubbleColor color)
    {
        GameObject powerUp = _poolManager.SpawnFromPool(powerUpTag, position, Quaternion.identity);
        powerUp.GetComponent<PowerUp>().SetColor(color); // Assume power-up also uses the BubbleColor system
    }

    private void SpawnOnBeat()
    {
        SpawnBubbles();
    }
}

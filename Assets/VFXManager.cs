using UnityEngine;

public class VFXManager : MonoBehaviour
{
    // VFX prefabs
    public string bubblePopTag = "BubblePop";      // Tag for Bubble Pop VFX
    public string idleTag = "LateVFX";             // Tag for Idle VFX
    public string perfectTag = "PerfectVFX";       // Tag for Perfect VFX
    public string goodTag = "GoodVFX";             // Tag for Good VFX
    public string missedTag = "MissedVFX";         // Tag for Missed VFX

    private PoolManager _poolManager;

    private void Start()
    {
        // Find PoolManager in the scene
        _poolManager = FindObjectOfType<PoolManager>();
    }

    // Function to spawn VFX at a given position
    public void SpawnVFX(string tag, Vector3 position)
    {
        GameObject vfx = _poolManager.SpawnFromPool(tag, position, Quaternion.identity);
        if (vfx != null)
        {
            // Optionally, you can add a destroy timer to auto-clean up the VFX after it plays
            Destroy(vfx, 2f);  // Destroy after 2 seconds (adjust as needed)
        }
    }

    // Function to handle bubble pop event
    public void PlayBubblePopVFX(Vector3 position)
    {
        //SpawnVFX(bubblePopTag, position);
    }

    // Function to handle idle event
    public void PlayLateVFX(Vector3 position)
    {
        SpawnVFX(idleTag, position);
    }

    // Function to handle perfect event
    public void PlayPerfectVFX(Vector3 position)
    {
        SpawnVFX(perfectTag, position);
    }

    // Function to handle good event
    public void PlayGoodVFX(Vector3 position)
    {
        SpawnVFX(goodTag, position);
    }

    // Function to handle missed event
    public void PlayMissedVFX(Vector3 position)
    {
        SpawnVFX(missedTag, position);
    }
}
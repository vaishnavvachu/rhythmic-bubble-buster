using UnityEngine;
using UnityEngine.EventSystems;

public class Bubble : MonoBehaviour, ICollidable
{
    public float moveSpeed = 7.5f; 
    public Vector3 direction;
    public float deactivateZ = -10f; 

    public float perfectWindow = 0.2f; // 200ms for perfect
    public float goodWindow = 0.5f;   // 500ms for good
    public float lateWindow = 0.8f;   // 800ms for late
    //public float totalLifetime = 3f; // Time before the bubble disappears

    private float spawnTime;
    

    private ICollidable _collidableImplementation;
    private Material _bubbleMaterial;
    private BubbleColor _currentBubbleColor;
    private VFXManager vfxManager;
    void Start()
    {
        direction = new Vector3(0, 0, -1);
        spawnTime = Time.time;
        vfxManager = FindObjectOfType<VFXManager>();
       
    }

    public string EvaluateTiming()
    {
        float currentTime = Time.time;
        float elapsedTime = currentTime - spawnTime;

        if (elapsedTime <= perfectWindow)
            return "Perfect";
        else if (elapsedTime <= goodWindow)
            return "Good";
        else if (elapsedTime <= lateWindow)
            return "Late";
        else
            return "Missed";
    }
    void Update()
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime);
        if (transform.position.z < deactivateZ)
        {
            gameObject.SetActive(false);
            ScoreManager.Instance.AddScore(-5);
            //_uiManager.AddScore(-5);// Penalty for missed bubble
            vfxManager.PlayMissedVFX(transform.position);
        }
       
    }

    public void SetColor(BubbleColor bubbleColor)
    {
        _currentBubbleColor = bubbleColor;
        _bubbleMaterial = GetComponent<Renderer>().material;
        if (_bubbleMaterial != null)
        {
            _bubbleMaterial.color = bubbleColor.ToUnityColor();
        }
    }

    public void PlaySFX()
    {
        throw new System.NotImplementedException();
    }

    public void PlayVFX()
    {
        throw new System.NotImplementedException();
    }

    public void OnCollide(BubbleColor bubbleColor)
    {
      
        if (bubbleColor == _currentBubbleColor)
        {
            string result = "Perfect";

            switch (result)
            {
                case "Perfect":
                    ScoreManager.Instance.AddScore(10);
                    //.AddScore(10);// Perfect score
                    vfxManager.PlayPerfectVFX(transform.position);
                    break;
                case "Good":
                    ScoreManager.Instance.AddScore(5);
                    //(5);// Good score
                    vfxManager.PlayGoodVFX(transform.position);
                    break;
                case "Late":
                    ScoreManager.Instance.AddScore(20);
                    //(5);// Late score
                    vfxManager.PlayLateVFX(transform.position);
                    break;
            }
            //vfxManager.PlayBubblePopVFX(transform.position);
            gameObject.SetActive(false);
        }
        else
        {
            ScoreManager.Instance.AddScore(-5);
            //_uiManager.AddScore(-5);// Penalty for missed bubble
            vfxManager.PlayMissedVFX(transform.position);
        }
    }
}
using UnityEngine;
using UnityEngine.EventSystems;

public class Bubble : MonoBehaviour, ICollidable
{
    public float moveSpeed = 5f; 
    
    public Vector3 direction;
    private ICollidable _collidableImplementation;
    private Material _bubbleMaterial;
    private BubbleColor _currentBubbleColor;
    void Start()
    {
        direction = new Vector3(0, 0, -1);
    }

    void Update()
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime);
        
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
            Destroy(gameObject); 
            Debug.Log("Bubble popped!");
        }
        else
        {
            Debug.Log("Missed or wrong color!");
        }
    }
}
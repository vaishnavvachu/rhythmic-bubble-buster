using System;
using UnityEngine;

public class PowerUp : MonoBehaviour,ICollidable
{
    public BubbleColor newSpikeColor = BubbleColor.Blue;
    public float moveSpeed = 5f; 
    public Vector3 direction;
    public float deactivateZ = -10f;
    
    private Material _powerupMaterial;

    private void Start()
    {
        SetColor(newSpikeColor);
        direction = new Vector3(0, 0, -1);
    }
    void Update()
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime);
        
        if (transform.position.z < deactivateZ)
        {
            gameObject.SetActive(false); 
        }
    }
    public void SetColor(BubbleColor bubbleColor)
    {
        _powerupMaterial = GetComponent<Renderer>().material;
        if (_powerupMaterial != null)
        {
            _powerupMaterial.color = bubbleColor.ToUnityColor();
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
        SpikeController spike = FindFirstObjectByType<SpikeController>();
        if (spike != null)
        {
            spike.ChangeSpikeColor(newSpikeColor);
            gameObject.SetActive(false);
            //Destroy(gameObject);
            Debug.Log($"Spike color changed to {newSpikeColor}");
        }
    }
}
using System.Collections;
using UnityEngine;

public class coinScript : MonoBehaviour
{
    [Header("Coin Settings")]
    public bool isCoinCollected = false;
    public AudioClip collectSound; // Optional sound effect
    
    [Header("Visual Effects")]
    public GameObject collectEffect; // Optional particle effect
    public float fadeOutTime = 0.3f;
    
    // Static variable to track if ANY coin has been collected in the level
    public static bool hasCollectedCoin = false;
    
    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        // Reset coin collection status at start of level
        hasCollectedCoin = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Optional: Make coin rotate for visual appeal
        transform.Rotate(0, 0, 90 * Time.deltaTime);
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if player collided with the coin
        if (other.CompareTag("Player") && !isCoinCollected)
        {
            CollectCoin();
        }
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        // Alternative collision detection if using regular colliders
        if (other.gameObject.CompareTag("Player") && !isCoinCollected)
        {
            CollectCoin();
        }
    }
    
    void CollectCoin()
    {
        // Mark this coin as collected
        isCoinCollected = true;
        
        // Set global game variable
        hasCollectedCoin = true;
        
        Debug.Log("Coin collected! Player can now complete the level.");
        
        // Play sound effect if available
        if (collectSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(collectSound);
        }
        
        // Spawn particle effect if available
        if (collectEffect != null)
        {
            Instantiate(collectEffect, transform.position, Quaternion.identity);
        }
        
        // Start disappearing animation
        StartCoroutine(DisappearCoin());
    }
    
    IEnumerator DisappearCoin()
    {
        // Fade out the coin
        float elapsedTime = 0;
        Color originalColor = spriteRenderer.color;
        
        while (elapsedTime < fadeOutTime)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeOutTime);
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }
        
        // Destroy the coin GameObject
        Destroy(gameObject);
    }
    
    // Public method to check if coin has been collected (for other scripts)
    public static bool IsCoinCollected()
    {
        return hasCollectedCoin;
    }
    
    // Method to reset coin collection status (call when restarting level)
    public static void ResetCoinCollection()
    {
        hasCollectedCoin = false;
    }
}

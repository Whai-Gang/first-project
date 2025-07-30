using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exitScript : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    
    // Start is called before the first frame update
    void Start()
    {
        // Get the sprite renderer and store original color
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if player entered the exit trigger
        if (other.CompareTag("Player"))
        {
            if (coinScript.hasCollectedCoin == true)
            {
                Debug.Log("Level complete!");
                
                // Change sprite color to black
                if (spriteRenderer != null)
                {
                    spriteRenderer.color = Color.black;
                }
                
                // Add level completion logic here
                // Example: SceneManager.LoadScene("NextLevel");
            }
            else
            {
                Debug.Log("Collect the coin first!");
            }
        }
    }
}

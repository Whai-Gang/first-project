using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class logicScript : MonoBehaviour
{
    [Header("Door System")]
    public GameObject door;
    public float doorOpenDistance = 3f;
    public float doorSpeed = 2f;
    public bool isDoorOpen = false;
    
    [Header("Button Settings")]
    public bool isButton = false;
    public Sprite buttonSprite; // Single sprite, change color instead
    public Color unpressedColor = Color.white;
    public Color pressedColor = Color.gray;
    
    private Vector3 doorClosedPosition;
    private Vector3 doorOpenPosition;
    private SpriteRenderer buttonRenderer;
    private bool isPressed = false;
    
    // Start is called before the first frame update
    void Start()
    {
        // If this is a button, get the sprite renderer
        if (isButton)
        {
            buttonRenderer = GetComponent<SpriteRenderer>();
            if (buttonRenderer != null)
            {
                if (buttonSprite != null)
                    buttonRenderer.sprite = buttonSprite;
                buttonRenderer.color = unpressedColor;
            }
        }
        
        // If we have a door, store its positions
        if (door != null)
        {
            doorClosedPosition = door.transform.position;
            doorOpenPosition = doorClosedPosition + Vector3.up * doorOpenDistance;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Animate door movement
        if (door != null)
        {
            Vector3 targetPosition = isDoorOpen ? doorOpenPosition : doorClosedPosition;
            door.transform.position = Vector3.MoveTowards(door.transform.position, targetPosition, doorSpeed * Time.deltaTime);
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
    // Check if player collided with this object
    if (other.CompareTag("Player") && isButton)
    {
      ActivateButton();
            Console.WriteLine("Button pressed!");
        }
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        // Alternative collision detection if using regular colliders
        if (other.gameObject.CompareTag("Player") && isButton)
        {
            ActivateButton();
        }
    }
    
    void ActivateButton()
    {
        if (!isPressed)
        {
            isPressed = true;
            isDoorOpen = !isDoorOpen; // Toggle door state
            
            // Change button color
            if (buttonRenderer != null)
            {
                buttonRenderer.color = pressedColor;
            }
            
            Debug.Log("Button activated! Door is now " + (isDoorOpen ? "opening" : "closing"));
            
            // Reset button after a short delay
            StartCoroutine(ResetButton());
        }
    }
    
    IEnumerator ResetButton()
    {
        yield return new WaitForSeconds(0.5f);
        isPressed = false;
        
        // Reset button color
        if (buttonRenderer != null)
        {
            buttonRenderer.color = unpressedColor;
        }
    }
}

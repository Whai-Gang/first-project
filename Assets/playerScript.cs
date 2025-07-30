using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    
    [Header("Camera Follow")]
    public Camera followCamera;
    public float cameraSpeed = 2f;
    public Vector3 cameraOffset = new Vector3(0, 2, -10);
    public bool smoothFollow = true;
    
    private Rigidbody2D rb;
    private float horizontalInput;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        // Auto-assign main camera if not set
        if (followCamera == null)
        {
            followCamera = Camera.main;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Get input
        horizontalInput = Input.GetAxisRaw("Horizontal");
        
        // Simple jump input
        if (Input.GetKeyDown(KeyCode.W))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        
        // Update camera position
        UpdateCameraFollow();
    }
    
    void FixedUpdate()
    {
        // Apply horizontal movement
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
    }
    
    void UpdateCameraFollow()
    {
        if (followCamera != null)
        {
            Vector3 targetPosition = transform.position + cameraOffset;
            
            if (smoothFollow)
            {
                // Smooth camera movement
                followCamera.transform.position = Vector3.Lerp(
                    followCamera.transform.position, 
                    targetPosition, 
                    cameraSpeed * Time.deltaTime
                );
            }
            else
            {
                // Instant camera movement
                followCamera.transform.position = targetPosition;
            }
        }
    }
}

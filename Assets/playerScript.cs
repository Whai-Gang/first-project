using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    
    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayerMask;
    
    [Header("Camera Follow")]
    public Camera followCamera;
    public float cameraSpeed = 2f;
    public Vector3 cameraOffset = new Vector3(0, 2, -10);
    public bool smoothFollow = true;
    
    private Rigidbody2D rb;
    private bool isGrounded;
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
        
        // Create ground check if it doesn't exist
        if (groundCheck == null)
        {
            GameObject groundCheckObj = new GameObject("GroundCheck");
            groundCheckObj.transform.SetParent(transform);
            groundCheckObj.transform.localPosition = new Vector3(0, -0.5f, 0);
            groundCheck = groundCheckObj.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Get input
        horizontalInput = Input.GetAxisRaw("Horizontal");
        
        // Check if grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayerMask);
        
        // Jump input
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            Jump();
        }
        
        // Update camera position
        UpdateCameraFollow();
    }
    
    void FixedUpdate()
    {
        // Apply horizontal movement
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
    }
    
    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
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
    
    void OnDrawGizmosSelected()
    {
        // Draw ground check circle in editor
        if (groundCheck != null)
        {
            Gizmos.color = isGrounded ? Color.green : Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}

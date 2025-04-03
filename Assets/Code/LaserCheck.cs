using UnityEngine;

public class LaserCheck : MonoBehaviour
{
    public int rayDistance = 10;
    public LayerMask controllerLayer;
    public int damageAmount = 10;
    private LineRenderer lineRenderer;
    private ScoreDisplay scoreDisplay;
    
    // Add debug option
    public bool showDebugInfo = true;
    
    // Cached raycast hit info
    private RaycastHit hitInfo;
    // Flag to prevent multiple hits per frame
    private bool hitProcessedThisFrame = false;
    // Add slight cooldown between hits to prevent rapid score loss
    public float hitCooldown = 0.5f;
    private float lastHitTime = 0f;

    void Start()
    {
        // Get the line renderer component
        lineRenderer = GetComponent<LineRenderer>();
        
        if (lineRenderer == null)
        {
            Debug.LogError("No Line Renderer found on laser!");
            return;
        }

        // Find the ScoreDisplay in the scene
        scoreDisplay = FindObjectOfType<ScoreDisplay>();
        if (scoreDisplay == null)
        {
            Debug.LogError("No ScoreDisplay found in scene!");
        }
        
        if (showDebugInfo)
        {
            Debug.Log("Laser initialized with raycasting approach.");
        }
    }
    
    void Update()
    {
        // Reset hit flag at the beginning of each frame
        hitProcessedThisFrame = false;
        
        // Perform the raycast from the laser's position in its forward direction
        bool hitSomething = Physics.Raycast(transform.position, transform.forward, out hitInfo, rayDistance, ~0);
        
        // Update the line renderer to visualize the laser
        lineRenderer.SetPosition(0, transform.position);
        
        if (hitSomething)
        {
            // If ray hit something, set the end position of line to hit point
            lineRenderer.SetPosition(1, hitInfo.point);
            
            if (showDebugInfo)
            {
                Debug.Log($"Laser hit: {hitInfo.collider.gameObject.name}, Tag: {hitInfo.collider.gameObject.tag}, Layer: {LayerMask.LayerToName(hitInfo.collider.gameObject.layer)}");
                Debug.DrawLine(transform.position, hitInfo.point, Color.red);
            }
            
            // Check if we can process a hit (cooldown check)
            if (Time.time > lastHitTime + hitCooldown)
            {
                // Process the hit if it's a controller
                ProcessHit(hitInfo.collider);
            }
        }
        else
        {
            // If ray hit nothing, set the end position to max distance
            lineRenderer.SetPosition(1, transform.position + transform.forward * rayDistance);
            
            if (showDebugInfo)
            {
                Debug.DrawRay(transform.position, transform.forward * rayDistance, Color.green);
            }
        }
    }
    
    private void ProcessHit(Collider hitCollider)
    {
        if (hitProcessedThisFrame) return;
        
        // Use the controllerLayer to check if the collider is on the controller layer
        bool isControllerLayer = (controllerLayer.value & (1 << hitCollider.gameObject.layer)) > 0;
        
        // Check for XR controllers with more flexibility in naming
        if (hitCollider.gameObject.name.ToLower().Contains("hand") || 
            hitCollider.gameObject.name.ToLower().Contains("controller") ||
            hitCollider.CompareTag("PlayerController") ||
            isControllerLayer)
        {
            if (GameManager.Instance == null)
            {
                Debug.LogError("GameManager instance is null!");
                return;
            }
            
            // Decrease score
            GameManager.Instance.DecreaseScore(damageAmount);
            
            // Show temporary hit message using ScoreDisplay
            if (scoreDisplay != null)
            {
                scoreDisplay.ShowTemporaryMessage($"Hit! -${damageAmount} points");
            }
            
            Debug.Log($"Controller hit laser! Score reduced by {damageAmount}");
            
            // Set the hit flag and cooldown time
            hitProcessedThisFrame = true;
            lastHitTime = Time.time;
        }
    }
}
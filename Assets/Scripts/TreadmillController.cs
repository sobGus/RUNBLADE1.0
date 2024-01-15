using UnityEngine;

public class TreadmillController : MonoBehaviour
{
    public float treadmillSpeed = 5f;
    public float resetPositionY = 10f; // Adjust this value based on your scene

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0f, -treadmillSpeed);
    }

    private void Update()
    {
        // Check if the treadmill has moved below the reset position
        if (transform.position.y < resetPositionY)
        {
            // Reset the treadmill position to the top
            ResetPosition();
        }
    }

    private void ResetPosition()
    {
        // Calculate the new position at the top
        Vector2 newPosition = new Vector2(transform.position.x, resetPositionY);
        transform.position = newPosition;
    }
}

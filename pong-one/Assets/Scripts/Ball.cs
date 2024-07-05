using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float initialSpeed = 5f; // Initial speed of the ball
    public float minBounceAngle = 25f; // Minimum bounce angle (degree)
    public float maxBounceAngle = 75f; // Maximum bounce angle (degrees)
    private Rigidbody2D rb;
    private Vector2 initialDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        LaunchBall();
    }

    void LaunchBall()
    {
        // Choose a random direction for the initial launch
        float randomDirection = Random.Range(0, 2) == 0 ? -1 : 1;
        initialDirection = new Vector2(randomDirection, Random.Range(-1f, 1f)).normalized;
        rb.velocity = initialDirection * initialSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Ensure the ball maintains constant speed after a collision
        rb.velocity = rb.velocity.normalized * initialSpeed;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Paddle")
        {
            // Calculate reflection of velocity against surface
            Vector2 reflection = Vector2.Reflect(rb.velocity, collision.transform.up);

            // Set bounce angle
            float bounceAngle = Random.Range(minBounceAngle, maxBounceAngle);
            Quaternion rotation = Quaternion.Euler(0, 0, bounceAngle);
            Vector2 adjustedReflection = rotation * reflection;

            // Apply new velocity
            rb.velocity = adjustedReflection.normalized * initialSpeed;
        }
    }

    public void ResetBall()
    {
        // Stop the ball's movement
        rb.velocity = Vector2.zero;

        // Reset the ball's position to the center of the screen
        transform.position = Vector2.zero;

        // Launch the ball again after a short delay
        StartCoroutine(LaunchBallWithDelay(1f));
    }

    IEnumerator LaunchBallWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        LaunchBall();
    }
}

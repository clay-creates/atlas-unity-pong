using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float initialSpeed = 5f;
    public float speedIncrement = 0.1f;
    public float maxSpeed = 20f;
    private Rigidbody2D rb;
    private Vector2 initialDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        LaunchBall();
    }

    void LaunchBall()
    {
        float randomDirection = Random.Range(0, 2) == 0 ? -1 : 1;
        initialDirection = new Vector2(randomDirection, Random.Range(-1f, 1f)).normalized;
        rb.velocity = initialDirection * initialSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Adjust the ball's speed incrementally
        if (rb.velocity.magnitude < maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * (rb.velocity.magnitude + speedIncrement);
        }

        if (collision.gameObject.CompareTag("Paddle"))
        {
            // Calculate reflection angle based on where the ball hits the paddle
            Vector2 paddlePosition = collision.transform.position;
            Vector2 hitPoint = transform.position;
            float difference = hitPoint.y - paddlePosition.y;
            float hitFactor = difference / collision.collider.bounds.size.y;
            Vector2 direction = new Vector2(rb.velocity.x, hitFactor).normalized;
            rb.velocity = direction * Mathf.Min(rb.velocity.magnitude + speedIncrement, maxSpeed);
        }
        else
        {
            rb.velocity = rb.velocity.normalized * Mathf.Min(rb.velocity.magnitude + speedIncrement, maxSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Paddle"))
        {
            // Calculate reflection angle based on where the ball hits the paddle
            Vector2 paddlePosition = collision.transform.position;
            Vector2 hitPoint = transform.position;
            float difference = hitPoint.y - paddlePosition.y;
            float hitFactor = difference / collision.bounds.size.y;
            Vector2 direction = new Vector2(rb.velocity.x, hitFactor).normalized;
            rb.velocity = direction * rb.velocity.magnitude;
        }
        else if (collision.CompareTag("Goal"))
        {
            // Handle goal detection
            ResetBall();
        }
    }

    public void ResetBall()
    {
        rb.velocity = Vector2.zero;
        transform.position = Vector2.zero; // Adjust as needed
        initialSpeed = 5f; // Reset the speed to the initial value
        StartCoroutine(LaunchBallWithDelay(1f));
    }

    IEnumerator LaunchBallWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        LaunchBall();
    }
}

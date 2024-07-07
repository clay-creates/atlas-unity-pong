using UnityEngine;

public class AIPlayer : MonoBehaviour
{
    public float speed = 5f;
    public float topBound = 4.5f;
    public float bottomBound = -4.5f;
    private Transform ball;
    private Rigidbody2D ballRb;

    void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Ball").transform;
        ballRb = ball.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (ballRb.velocity.x < 0) // Ball is moving towards the AI
        {
            if (ball.position.y > transform.position.y)
            {
                MovePaddle(Vector2.up);
            }
            else if (ball.position.y < transform.position.y)
            {
                MovePaddle(Vector2.down);
            }
        }

        ClampPaddlePosition();
    }

    void MovePaddle(Vector2 direction)
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void ClampPaddlePosition()
    {
        Vector3 clampedPosition = transform.position;
        clampedPosition.y = Mathf.Clamp(transform.position.y, bottomBound, topBound);
        transform.position = clampedPosition;
    }
}

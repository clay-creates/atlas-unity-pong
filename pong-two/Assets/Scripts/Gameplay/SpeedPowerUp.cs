using System.Collections;
using UnityEngine;
using ZPong;

public class SpeedPowerUp : MonoBehaviour
{
    public float speedMultiplier = 2f;
    public float duration = 5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            Ball activeBall = FindObjectOfType<Ball>();
            if (activeBall != null)
            {
                StartCoroutine(ApplySpeedUp(activeBall));
            }
        }
    }

    private IEnumerator ApplySpeedUp(Ball ball)
    {
        ball.ModifySpeed(speedMultiplier);
        Destroy(gameObject); // Destroy the power-up after it's collected
        yield return new WaitForSeconds(duration);
        ball.ModifySpeed(1f / speedMultiplier); // Revert speed after duration
    }
}

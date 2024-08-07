using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public ParticleSystem paddleHitEffect;
    // public ParticleSystem powerUpEffect;

    public void PlayPaddleHitEffect(Vector2 position)
    {
        if (paddleHitEffect != null)
        {
            paddleHitEffect.transform.position = new Vector3(position.x, position.y, 0);
            paddleHitEffect.Play();
        }
        else
        {
            Debug.LogError("Paddle Hit effect is not assigned!");
        }
    }

    /*public void PlayPowerUpEffect(Vector2 position)
    {
        if (powerUpEffect != null)
        {
            powerUpEffect.transform.position = new Vector3(position.x, position.y, 0);
            powerUpEffect.Play();
        }
        else
        {
            Debug.LogError("Power-up effect is not assigned!");
        }
    }*/
}

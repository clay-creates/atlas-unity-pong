using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZPong
{
    public class Ball : MonoBehaviour
    {
        public float speed = 5f;
        public Color slowColor = Color.blue;
        public Color fastColor = Color.red;
        public float maxSpeed = 20f;

        private float screenTop = 527;
        private float screenBottom = -527;

        private Vector2 direction;
        private Vector2 defaultDirection;

        private bool ballActive;

        protected RectTransform rectTransform;

        private AudioSource bounceSFX;
        private Animator animator;
        private Rigidbody2D rb;
        private Renderer ballRenderer;

        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            rb = GetComponent<Rigidbody2D>();
            ballRenderer = GetComponent<Renderer>();

            if (PlayerPrefs.HasKey("BallSpeed"))
            {
                speed = PlayerPrefs.GetFloat("BallSpeed");
            }

            if (PlayerPrefs.HasKey("BallSize"))
            {
                var value = PlayerPrefs.GetFloat("BallSize");
                rectTransform.sizeDelta = new Vector2(value, value);
            }

            if (PlayerPrefs.HasKey("PitchDirection"))
            {
                string pitchDirectionValue = PlayerPrefs.GetString("PitchDirection");

                if (pitchDirectionValue == "Random")
                {
                    float randomX = Random.Range(-1f, 1f);
                    direction = new Vector2(randomX, 0f).normalized;
                }
                else if (pitchDirectionValue == "Right")
                {
                    direction = new Vector2(1f, 0f);
                }
                else
                {
                    direction = new Vector2(-1f, 0f);
                }
            }
            else
            {
                direction = new Vector2(-1f, 0f);
            }

            defaultDirection = direction;

            SetHeightBounds();

            bounceSFX = this.GetComponent<AudioSource>();
            animator = GetComponent<Animator>();

            // Start the coroutine to delay the ball activation
            StartCoroutine(ActivateBallAfterDelay(.5f));
        }

        private void Update()
        {
            if (ballActive)
            {
                Vector2 newPosition = rectTransform.anchoredPosition + (direction * speed * Time.deltaTime);
                rectTransform.anchoredPosition = newPosition;

                if (rectTransform.anchoredPosition.y >= screenTop || rectTransform.anchoredPosition.y <= screenBottom)
                {
                    direction.y *= -1f;
                    PlayBounceSound();
                    TriggerScreenShake();
                }

                // Update ball color
                float currentSpeed = rb.velocity.magnitude;
                UpdateBallColor(currentSpeed);
            }
        }

        protected void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Paddle"))
            {
                Paddle paddle = collision.gameObject.GetComponent<Paddle>();

                float y = BallHitPaddleWhere(GetPosition(), paddle.AnchorPos(),
                    paddle.GetComponent<RectTransform>().sizeDelta.y / 2f);
                Vector2 newDirection = new Vector2(paddle.isLeftPaddle ? 1f : -1f, y);

                Reflect(newDirection);
                PlayBounceSound();
                TriggerScreenShake();

                // Trigger impact animation
                animator.SetTrigger("Impact");
            }
            else if (collision.gameObject.CompareTag("Goal"))
            {
                if (this.rectTransform.anchoredPosition.x < -1)
                {
                    ScoreManager.Instance.ScorePointPlayer2();
                }
                else
                {
                    ScoreManager.Instance.ScorePointPlayer1();
                }
            }
        }

        private void TriggerScreenShake()
        {
            // Implement screen shake logic here
        }

        public void Reflect(Vector2 newDirection)
        {
            direction = newDirection.normalized;
        }

        public void SetBallActive(bool value)
        {
            ballActive = value;
            direction = defaultDirection;
        }

        public Vector2 GetPosition()
        {
            return rectTransform.anchoredPosition;
        }

        public void SetHeightBounds()
        {
            var height = UIScaler.Instance.GetUIHeightPadded();
            screenTop = height / 2;
            screenBottom = -1 * height / 2;
        }

        protected float BallHitPaddleWhere(Vector2 ball, Vector2 paddle, float paddleHeight)
        {
            return (ball.y - paddle.y) / paddleHeight;
        }

        void PlayBounceSound()
        {
            bounceSFX.pitch = Random.Range(.8f, 1.2f);
            bounceSFX.Play();
        }

        public void DisableBall()
        {
            ballActive = false;
        }

        // Coroutine to activate the ball after a delay
        public IEnumerator ActivateBallAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            SetBallActive(true);
        }

        private void UpdateBallColor(float speed)
        {
            Color ballColor = Color.Lerp(slowColor, fastColor, speed / maxSpeed);
            ballRenderer.material.color = ballColor;
        }

        public void ModifySpeed(float multiplier)
        {
            rb.velocity *= multiplier;
        }
    }
}
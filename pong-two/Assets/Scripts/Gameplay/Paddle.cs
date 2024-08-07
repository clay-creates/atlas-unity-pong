using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace ZPong
{
    [RequireComponent(typeof(Collider2D))]
    public class Paddle : MonoBehaviour
    {
        public bool isLeftPaddle = true;
        public float lerpSpeed = 10f; // Speed of the lerp

        private float halfPlayerHeight;
        public float screenTop { get; private set; }
        public float screenBottom { get; private set; }

        private RectTransform rectTransform;
        public Animator animator;

        private Vector2 targetPosition;

        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();

            if (PlayerPrefs.HasKey("PaddleSize"))
            {
                rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, PlayerPrefs.GetFloat("PaddleSize"));
                this.GetComponent<BoxCollider2D>().size = rectTransform.sizeDelta;
            }

            halfPlayerHeight = rectTransform.sizeDelta.y / 2f;

            var height = UIScaler.Instance.GetUIHeight();

            screenTop = height / 2;
            screenBottom = -1 * height / 2;

            targetPosition = rectTransform.anchoredPosition; // Initialize target position
        }

        private void Update()
        {
            // Smoothly move towards the target position using Lerp
            rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, targetPosition, Time.deltaTime * lerpSpeed);
        }

        public void Move(float movement)
        {
            targetPosition.y += movement;
            targetPosition.y = Mathf.Clamp(targetPosition.y, screenBottom + halfPlayerHeight, screenTop - halfPlayerHeight);
        }

        public float GetHalfHeight()
        {
            return halfPlayerHeight;
        }

        public Vector2 AnchorPos()
        {
            return rectTransform.anchoredPosition;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log("Collision detected with: " + collision.gameObject.name);
            if (collision.gameObject.CompareTag("Ball"))
            {
                Debug.Log("Ball hit the paddle, setting Impact trigger.");
                animator.SetTrigger("Impact");

                // Get the ball's speed and trigger the screen shake
                Rigidbody2D ballRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
                if (ballRigidbody != null)
                {
                    float speed = ballRigidbody.velocity.magnitude;
                    ScreenShake.Instance.Shake(0.2f, speed * 0.1f); // Adjust the shake duration and magnitude as needed
                }
            }
        }
    }
}

using System.Collections;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public static ScreenShake Instance { get; private set; }

    private Vector3 originalPosition;
    private float shakeDuration;
    private float shakeMagnitude;
    private float shakeElapsed;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        Debug.Log("ScreenShake Awake");
    }

    private void Start()
    {
        originalPosition = transform.localPosition;
        Debug.Log("ScreenShake Start");
    }

    public void Shake(float duration, float magnitude)
    {
        Debug.Log("Shake called with duration: " + duration + " and magnitude: " + magnitude);
        shakeDuration = duration;
        shakeMagnitude = magnitude;
        shakeElapsed = 0.0f;
        StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        while (shakeElapsed < shakeDuration)
        {
            shakeElapsed += Time.deltaTime;

            Vector3 randomPoint = originalPosition + Random.insideUnitSphere * shakeMagnitude;
            transform.localPosition = new Vector3(randomPoint.x, randomPoint.y, originalPosition.z);

            Debug.Log("Camera Position: " + transform.localPosition);

            yield return null;
        }

        transform.localPosition = originalPosition;
        Debug.Log("Camera Position Reset: " + transform.localPosition);
    }
}

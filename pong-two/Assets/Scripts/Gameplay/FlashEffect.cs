using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FlashEffect : MonoBehaviour
{
    private Image flashImage;
    private bool isFlashing = false;

    private void Awake()
    {
        flashImage = GetComponentInChildren<Image>();
    }

    public void TriggerFlash()
    {
        if (!isFlashing)
        {
            StartCoroutine(Flash());
        }
    }

    private IEnumerator Flash()
    {
        isFlashing = true;

        // Set the image to fully opaque
        flashImage.color = new Color(flashImage.color.r, flashImage.color.g, flashImage.color.b, 1);

        // Wait for one frame
        yield return null;

        // Set the image back to fully transparent
        flashImage.color = new Color(flashImage.color.r, flashImage.color.g, flashImage.color.b, 0);

        isFlashing = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedIndicator : MonoBehaviour
{
    public Text speedText;
    public Rigidbody2D ball;

    // Update is called once per frame
    void Update()
    {
        float speed = ball.velocity.magnitude;
        speedText.text = $"Speed: {speed:F2}";
    }
}

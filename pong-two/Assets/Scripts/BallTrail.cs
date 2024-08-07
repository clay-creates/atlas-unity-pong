using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTrail : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Transform ball;

    private Queue<Vector3> trailPositions;
    public int trailLength = 10; // Number of points in the trail
    public Gradient trailColor;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        ball = transform;
        lineRenderer.positionCount = trailLength;
        lineRenderer.colorGradient = trailColor;
        trailPositions = new Queue<Vector3>();

        // Initialize the trail positions
        for (int i = 0; i < trailLength; i++)
        {
            trailPositions.Enqueue(ball.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Add the current ball position to the queue
        trailPositions.Enqueue(ball.position);

        // Remove the oldest position if the trail is longer than desired
        if (trailPositions.Count > trailLength)
        {
            trailPositions.Dequeue();
        }

        // Update the LineRenderer positions
        Vector3[] positions = trailPositions.ToArray();
        lineRenderer.SetPositions(positions);
    }
}

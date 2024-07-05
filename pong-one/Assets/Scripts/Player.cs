using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] public KeyCode upKey;
    [SerializeField] public KeyCode downKey;

    public GameObject player;

    public float paddleSpeed = 10f;

    // Update is called once per frame
    void Update()
    {
        MovePaddle();
    }

    void MovePaddle()
    {
        float verticalInput = 0;
        if (Input.GetKey(upKey))
        {
            verticalInput = 1;
        }
        else if (Input.GetKey(downKey))
        {
            verticalInput = -1;
        }

        transform.Translate(new Vector2(0, verticalInput * paddleSpeed * Time.deltaTime));
    }
}

using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public KeyCode upKey;
    [SerializeField] public KeyCode downKey;

    public float paddleSpeed = 10f;
    public float verticalInput;

    public float topBound = 4.5f;
    public float bottomBound = -4.5f;

    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }*/
    }

    private void FixedUpdate()
    {
        MovePaddle();
/*
        if (transform.position.y > topBound)
        {
            transform.position = new Vector3(transform.position.x, topBound, 0);
        }
        else if (transform.position.y < bottomBound)
        {
            transform.position = new Vector3(transform.position.x, bottomBound, 0);
        }*/
    }

    void MovePaddle()
    {
        verticalInput = 0;

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

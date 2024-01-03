using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 2.5f;
    [SerializeField]
    private float flapHeight = 4f;
    [SerializeField]
    private float maxFallingVelocity = -100f;
    [SerializeField]
    public int score = 0;
    [SerializeField]
    public double timeLimit = 100.0;

    public double time;
    public Rigidbody2D rb;
    private float yVelocity = 0.0f;
    public bool gameOver = false;
    public float height;
    public float distance;
    private bool diving = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        time = Time.time;
        if (time > timeLimit)
        {
            gameOver = true;
        }
        if (!gameOver)
        {
            if ((Input.GetKeyDown("w") || Input.GetKeyDown(KeyCode.UpArrow)) && !diving)
            {
                yVelocity = flapHeight;
                rb.velocity = new Vector2(speed, yVelocity);
            }
            else if ((Input.GetKeyDown("s") || Input.GetKeyDown(KeyCode.DownArrow)) && !diving)
            {
                yVelocity = Mathf.Pow(Mathf.Abs(rb.velocity.y), 1.2f) * -1f;
                if (yVelocity < maxFallingVelocity)
                {
                    yVelocity = maxFallingVelocity;
                }
                rb.velocity = new Vector2(speed, yVelocity);
                diving = true;
            }
            else if ((Input.GetKeyDown("s") || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown("w") || Input.GetKeyDown(KeyCode.UpArrow)) && diving)
            {
                yVelocity = flapHeight;
                rb.velocity = new Vector2(speed, yVelocity);
                diving = false;
            }
            height = transform.position.y;
            distance = transform.position.x;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // speed boost
        if (collision.gameObject.CompareTag("Finish"))
        {
            rb.velocity = new Vector2(rb.velocity.x + 1, rb.velocity.y);
            score += 1;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 1.0f;
    [SerializeField]
    private float flapHeight = 10.0f;

    public Rigidbody2D rb;
    private float yVelocity = 0.0f;
    public bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            if (Input.GetKeyDown("w") || Input.GetKeyDown(KeyCode.UpArrow))
            {
                yVelocity = flapHeight;
                rb.velocity = new Vector2(speed, yVelocity);
            }
            else if (Input.GetKeyDown("s") || Input.GetKeyDown(KeyCode.DownArrow))
            {
                yVelocity = Mathf.Pow(yVelocity, 1.2f) * -1;
                rb.velocity = new Vector2(speed, yVelocity);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // speed boost
        if (collision.gameObject.CompareTag("Finish"))
        {
            rb.velocity = new Vector2(rb.velocity.x + 1, rb.velocity.y);
        }
    }

}

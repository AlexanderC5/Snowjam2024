using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 1.0f;
    [SerializeField]
    private float flapHeight = 10.0f;
    [SerializeField]
    public float gravityScaler = 1.0f;

    public Rigidbody2D rb;
    private float yVelocity = 0.0f;
    public bool gameOver = false;
    


    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        //rb.gravityScale = gravityScaler;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown("w") || Input.GetKeyDown(KeyCode.UpArrow)) && !gameOver)
        {
            yVelocity = flapHeight;
            rb.velocity = new Vector2(speed, yVelocity);
        }
        else if ((Input.GetKeyDown("s") || Input.GetKeyDown(KeyCode.DownArrow)) && !gameOver)
        {
            yVelocity = Mathf.Pow(yVelocity, 1.2f) * -1;
            rb.velocity = new Vector2(speed, yVelocity);
        }
        rb.gravityScale = gravityScaler;

        /*else
        {
            yVelocity = rb.gravityScale;
        }*/
        //rb.velocity = new Vector2(speed, yVelocity);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("TagOfCollidingObject"))
        {
            gameOver = true;
            rb.velocity = new Vector2(0, 0);
        }
    }
}

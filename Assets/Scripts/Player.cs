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
        //rb.velocity = new Vector2(0, 0);
        //rb.gravityScale = gravityScaler;
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
                //rb.rotation = 45f;
            }
            else if (Input.GetKeyDown("s") || Input.GetKeyDown(KeyCode.DownArrow))
            {
                yVelocity = Mathf.Pow(yVelocity, 1.2f) * -1;
                rb.velocity = new Vector2(speed, yVelocity);
                //rb.rotation = -45f;
            }
            rb.gravityScale = gravityScaler;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (collision.gameObject.CompareTag("Finish"))
        {
            gameOver = true;
            rb.velocity = new Vector2(0, 0);
        }*/
        if (collision.gameObject.CompareTag("Finish"))
        {
            rb.velocity = new Vector2(rb.velocity.x + 1, rb.velocity.y);
            //GameObject.FindWithTag("Player").GetComponent<Player>().rb.velocity = new Vector2(GameObject.FindWithTag("Player").GetComponent<Player>().rb.velocity.x + 1, GameObject.FindWithTag("Player").GetComponent<Player>().rb.velocity.y);
        }

        /*
        if (gameObject.tag == "Player")
        {
            if (collision.gameObject.CompareTag("Finish"))
            {
                gameOver = true;
                rb.velocity = new Vector2(0, 0);
            }
        }
        else if(gameObject.tag == "Border")
        {
            if (collision.gameObject.CompareTag("Finish"))
            {
                rb.velocity = new Vector2(rb.velocity.x + 1, rb.velocity.y + 1);
            }
        }*/
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    [SerializeField]
    private string dir = "Down Left";
    [SerializeField]
    private float speed = 1f;
    private Rigidbody2D rb;
    private float x;
    private float y;
    private int hitGround = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        if (dir.Contains("Down"))
        {
            //Debug.Log("Added Force");
            y = -speed;
        }
        if (dir.Contains("Up"))
        {
            //Debug.Log("Added Force");
            //rb.AddForce(transform.up * speed);
            y = speed;
        }
        if (dir.Contains("Left"))
        {
            //Debug.Log("Added Force");
            //rb.AddForce(-transform.right * speed);
            x = -speed;
        }
        if (dir.Contains("Right"))
        {
            //Debug.Log("Added Force");
            //rb.AddForce(transform.right * speed);
            x = speed;
        }
        rb.velocity = new Vector2(x, y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("obstacle"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("ground") && hitGround < 10)
        {
            hitGround += 1;
        }
        else if (collision.gameObject.CompareTag("ground"))
        {
            Destroy(gameObject);
        }
    }
}

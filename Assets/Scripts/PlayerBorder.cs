using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBorder : MonoBehaviour
{
    //public GameObject player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //playerrb = GameObject.FindWithTag("Player").GetComponent<rb>;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        /*if (collision.gameObject.CompareTag("Finish"))
        {
            GameObject.FindWithTag("Player").GetComponent<Player>().rb.velocity = new Vector2(GameObject.FindWithTag("Player").GetComponent<Player>().rb.velocity.x + 1, GameObject.FindWithTag("Player").GetComponent<Player>().rb.velocity.y);
        }*/
        if (collision.gameObject.CompareTag("Finish"))
        {
            GameObject.FindWithTag("Player").GetComponent<Player>().gameOver = true;
            GameObject.FindWithTag("Player").GetComponent<Player>().rb.velocity = new Vector2(0, 0);
        }
    }
}
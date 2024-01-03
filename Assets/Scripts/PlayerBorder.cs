using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBorder : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            GameObject.FindWithTag("Player").GetComponent<Player>().gameOver = true;
            GameObject.FindWithTag("Player").GetComponent<Player>().rb.velocity = new Vector2(0, 0);
            GameObject.FindWithTag("Player").GetComponent<Player>().score -= 1;
        }
    }
}
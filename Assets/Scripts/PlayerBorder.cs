using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBorder : MonoBehaviour
{
    [SerializeField]
    private int coinWorth = 3;
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
            //GameObject.FindWithTag("Player").GetComponent<Player>().score -= 1;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            GameObject.FindWithTag("Player").GetComponent<Player>().score += coinWorth;
            GameObject.FindWithTag("Player").GetComponent<Player>().backgroundScore += coinWorth;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("flapHeightUpgrade"))
        {
            GameObject.FindWithTag("Player").GetComponent<Player>().flapHeightUpgrade += 0.25f;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("diveUpgrade"))
        {
            GameObject.FindWithTag("Player").GetComponent<Player>().diveUpgrade += 0.02f;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("speedUpgrade"))
        {
            GameObject.FindWithTag("Player").GetComponent<Player>().speedUpgrade += 0.5f;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("zone"))
        {
            GameObject.FindWithTag("Player").GetComponent<Player>().inZone = collision.gameObject.GetComponent<Zone>().zone;
            GameObject.FindWithTag("Player").GetComponent<Player>().Zone = collision.gameObject;
        }
    }
}
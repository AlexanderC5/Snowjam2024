using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBorder : MonoBehaviour
{
    [SerializeField]
    private int coinWorth = 3;
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player= GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("obstacle"))
        {
            player.animator.Play("Bonk");
            player.gameOver = true;
            player.rb.velocity = new Vector2(0, 0);
            StartCoroutine(Swap());
            //player.score -= 1;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            player.score += coinWorth;
            player.backgroundScore += coinWorth;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("flapHeightUpgrade"))
        {
            player.flapHeightUpgrade += 0.25f;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("diveUpgrade"))
        {
            player.diveUpgrade += 0.02f;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("speedUpgrade"))
        {
            player.speedUpgrade += 0.5f;
            player.speedTarget += 0.5f;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("zone"))
        {
            player.inZone = collision.gameObject.GetComponent<Zone>().zone;
            player.Zone = collision.gameObject;
        }
        else if (collision.gameObject.CompareTag("boost"))
        {
            player.rb.AddForce(collision.gameObject.GetComponent<BoostItem>().Direction * collision.gameObject.GetComponent<BoostItem>().Strength, ForceMode2D.Impulse);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Finish"))
        {
            SceneManager.LoadScene("Start");
        }
    }

    IEnumerator Swap()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Start");
    }
}
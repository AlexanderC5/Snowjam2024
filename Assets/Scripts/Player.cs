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

    public int backgroundScore = 0;
    public double time;
    public Rigidbody2D rb;
    private float yVelocity = 0.0f;
    public bool gameOver = false;
    public float height;
    public float distance;
    private bool diving = false;
    private float winDist = 50f;
    public bool win = false;
    public int inZone;
    public GameObject Zone;

    //upgrade counter
    public float diveUpgrade = 0f;
    public float flapHeightUpgrade = 0f;
    public float speedUpgrade = 0f;

    private bool wasHandled = false;
    private GameManager gameManager;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        //animator = GameObject.FindWithTag("SpritePlayer").GetComponent<Animator>();
        animator = transform.Find("SpritePlayer").GetComponent<Animator>();
        gameManager = Object.FindFirstObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        time = Time.time;
        if (distance >= winDist)
        {
            win = true;
            gameManager.GameState = GameManager.GameStates.win;
        }
        else if (time > timeLimit)
        {
            gameOver = true;
            animator.Play("Bonk");
            gameManager.GameState = GameManager.GameStates.lose;
        }

        // add to max time dependent on score
        if (backgroundScore >= 3)
        {
            timeLimit += 5;
            backgroundScore -= 3;
        }

        if (!gameOver)
        {
            if ((Input.GetKeyDown("w") || Input.GetKeyDown(KeyCode.UpArrow)) && !diving)
            {
                yVelocity = flapHeight + flapHeightUpgrade;
                rb.velocity = new Vector2(speed + speedUpgrade, yVelocity);
                //animator.SetTrigger("Flap");
                animator.Play("Flap");
            }
            else if ((Input.GetKeyDown("s") || Input.GetKeyDown(KeyCode.DownArrow)) && !diving)
            {
                yVelocity = Mathf.Pow(Mathf.Abs(rb.velocity.y), 1.2f + diveUpgrade) * -1f;
                if (yVelocity < maxFallingVelocity)
                {
                    yVelocity = maxFallingVelocity;
                }
                rb.velocity = new Vector2(speed + speedUpgrade, yVelocity);
                diving = true;
                animator.Play("Dive");
            }
            else if ((Input.GetKeyDown("s") || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown("w") || Input.GetKeyDown(KeyCode.UpArrow)) && diving)
            {
                yVelocity = flapHeight;
                rb.velocity = new Vector2(speed, yVelocity);
                diving = false;
                animator.Play("Flap");
            }

            if (rb.velocity.y <= maxFallingVelocity)
            {
                rb.velocity = new Vector2(rb.velocity.x, maxFallingVelocity);
            }

            height = transform.position.y;
            distance = transform.position.x;

            if(!wasHandled && rb.velocity.x != 0)
            {
                StartCoroutine(scoreAdd());
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // speed boost
        if (collision.gameObject.CompareTag("obstacle"))
        {
            rb.velocity = new Vector2(rb.velocity.x + 1, rb.velocity.y);
            //score += 1;
        }
    }

    private IEnumerator scoreAdd()
    {
        wasHandled = true;
        yield return new WaitForSeconds(1.0f);
        score += (int) speed;
        wasHandled = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float startSpeed = 5f;
    [SerializeField]
    private float flapHeight = 8f;
    [SerializeField]
    private float maxFallingVelocity = -100f;
    [SerializeField]
    public int score = 0;
    [SerializeField]
    public double timeLimit = 100.0;
    [SerializeField]
    private float winDist = 150f;


    public int backgroundScore = 0;
    public double time;
    public Rigidbody2D rb;
    private float yVelocity = 0.0f;
    public bool gameOver = false;
    public float height;
    public float distance;
    private bool diving = false;
    public bool win = false;
    public int inZone;
    public GameObject Zone;

    //upgrade counter
    public float diveUpgrade = 0f;
    public float flapHeightUpgrade = 0f;
    public float speedUpgrade = 0f;
    public float speedTarget = 5f;
    public float targetAccel = 0.25f;

    private bool wasHandled = false;
    private GameManager gameManager;

    public Animator animator;
    public AudioSource audioSource;
    public AudioClip[] audioClipArray;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = transform.Find("SpritePlayer").GetComponent<Animator>();
        gameManager = Object.FindFirstObjectByType<GameManager>();
        rb.velocity = new Vector2(startSpeed, rb.velocity.y);
        audioSource.PlayOneShot(audioClipArray[3]);
    }

    // Update is called once per frame
    void Update()
    {
        time = Time.time;
        speedTarget += targetAccel * Time.deltaTime;
        if (distance >= winDist)
        {
            win = true;
            gameManager.GameState = GameManager.GameStates.win;
            SceneManager.LoadScene("Win");
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
                rb.velocity = new Vector2(rb.velocity.x + speedUpgrade, yVelocity);
                //rb.AddForce(new Vector2(Mathf.Clamp((speedTarget-rb.velocity.x)*2, 0, 100), 0));
                rb.AddForce(new Vector2(Mathf.Clamp((speedTarget - rb.velocity.x) * 1.25f, 0, 100), 0));
                animator.Play("Flap");
            }
            else if ((Input.GetKeyDown("s") || Input.GetKeyDown(KeyCode.DownArrow)) && !diving)
            {
                yVelocity = Mathf.Pow(Mathf.Abs(rb.velocity.y), 1.1f + diveUpgrade) * -1f;
                //rb.AddForce(new Vector2(Mathf.Abs(yVelocity*0.25f), 0));
                if (yVelocity < maxFallingVelocity)
                {
                    yVelocity = maxFallingVelocity;
                }
                rb.velocity = new Vector2(rb.velocity.x + speedUpgrade, yVelocity);
                diving = true;
                animator.Play("Dive");
            }
            else if ((Input.GetKeyDown("s") || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown("w") || Input.GetKeyDown(KeyCode.UpArrow)) && diving)
            {
                flapHeight += 0.5f;
                yVelocity = flapHeight;
                rb.velocity = new Vector2(rb.velocity.x, yVelocity);
                rb.AddForce(new Vector2(Mathf.Abs(yVelocity * 0.25f), 0));
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

            if(rb.velocity.x <= 5)
            {
                rb.velocity = new Vector2(5, rb.velocity.y);
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
        score += (int) rb.velocity.x;
        wasHandled = false;
    }
}

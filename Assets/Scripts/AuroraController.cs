using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuroraController : MonoBehaviour
{
    [SerializeField] public float MaxDistance = 20f;
    [SerializeField] public float Speed = 0.5f;
    [SerializeField] public float Accleration = 0.1f;
    [SerializeField] public float StartingDistance = 15f;
    // Start is called before the first frame update
    private GameObject player;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        transform.position = player.transform.position - new Vector3(StartingDistance, 0, 0);
        // Debug.Log(transform.rotation.z);
    }

    void Update()
    {
        Vector2 nextPos = transform.position;
        nextPos.x += Speed * Time.deltaTime;

        // follow player y;
        Vector2 playerDisplacement = player.transform.position - transform.position;
        nextPos.x += playerDisplacement.y * Mathf.Tan(transform.rotation.z);
        nextPos.y += playerDisplacement.y;

        // stay within MaxDistance;
        if (playerDisplacement.x >= MaxDistance)
            nextPos.x =  player.transform.position.x - MaxDistance;

        transform.position = nextPos;
        Speed += Accleration * Time.deltaTime;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("End the game");
        }
    }
}
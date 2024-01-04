using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// if we want a different force profile (eg. a longer boost it should be done player-side)
public class BoostItem : MonoBehaviour
{
    [SerializeField] float Strength = 5f;
    [SerializeField] Vector2 Direction = new Vector2(1, 0);
    // Start is called before the first frame update
    void Start()
    {
        Direction = Direction.normalized;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Player")
        {
            collision.rigidbody.AddForce(Direction * Strength, ForceMode2D.Impulse);
            Debug.Log("Player picked up boost");
            Destroy(gameObject);
        }
    }
}

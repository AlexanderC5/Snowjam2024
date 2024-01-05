using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// if we want a different force profile (eg. a longer boost it should be done player-side)
public class BoostItem : MonoBehaviour
{
    [SerializeField] public float Strength = 5f;
    [SerializeField] public Vector2 Direction = new Vector2(1, 0);
    // Start is called before the first frame update
    void Start()
    {
        Direction = Direction.normalized;
    }

    /*void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Entered Collision");
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player picked up boost");
            collision.rigidbody.AddForce(Direction * Strength, ForceMode2D.Impulse);
            Destroy(gameObject);
        }
    }*/
}

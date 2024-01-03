using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuroraEffect : MonoBehaviour
{
    GameObject player;
    float playerHeight;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //this.GetComponent<ParticleSystem>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        playerHeight = player.transform.position.y;
    }
}

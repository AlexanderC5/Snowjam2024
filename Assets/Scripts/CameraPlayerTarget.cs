using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayerTarget : MonoBehaviour
{
    private Player player;
    private float playerVelocityY;
    [SerializeField] float leadPlayerX = 5f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        //Vector2 playerPos = player.transform.position;
        playerVelocityY = player.GetVelocityY();
        var adjustedVelocityY = Mathf.Clamp(playerVelocityY / 10f, -0.5f, 0.5f);
        this.transform.localPosition = new Vector2(leadPlayerX, adjustedVelocityY); //- this.transform.localPosition.y)/3f + this.transform.localPosition.y);
    }
}

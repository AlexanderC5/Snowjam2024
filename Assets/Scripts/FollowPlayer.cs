using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] public bool isEnabled;
    private GameObject player;
    private Vector2 playerPos;

    // Start is called before the first frame update
    void Start()
    {
        isEnabled = true;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {   
        if (isEnabled)
        {
            playerPos = new Vector2(player.transform.localPosition.x, player.transform.localPosition.y);
            this.transform.localPosition = new Vector3(playerPos.x, playerPos.y, this.transform.localPosition.z);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSegment : MonoBehaviour{
    // anchorOffset refers to how much to move the anchor by when spawning this object
    // spriteOffset refers to where to spawn the object in relation to the anchor

    [SerializeField] private Vector2 anchorOffset = new Vector2(-1, -1);
    [SerializeField] private Vector2 spriteOffset = new Vector2(-1, -1);
    public Vector2 AnchorOffset{get{return anchorOffset;}}
    public Vector2 SpriteOffset{get{return spriteOffset;}}
    void Awake(){
        if (anchorOffset.x == -1)
        {
            Debug.Log(string.Format("Anchor Offset x is calculated to be {0}", GetComponent<SpriteRenderer>().bounds.size.x));
            anchorOffset.x = GetComponent<SpriteRenderer>().bounds.size.x;
        }
        if (spriteOffset.x == -1)
        {
            Debug.Log(string.Format("Sprite Offset x is calculated to be {0}", GetComponent<SpriteRenderer>().bounds.size.x/2));
            spriteOffset.x = GetComponent<SpriteRenderer>().bounds.size.x / 2;
        }
    }
}
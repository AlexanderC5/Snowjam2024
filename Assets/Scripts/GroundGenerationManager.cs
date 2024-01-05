using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerationManager : MonoBehaviour
{

    [SerializeField] public bool Win = false;
    [SerializeField] private List<GameObject> mountainPrefabs;
    [SerializeField] private GameObject winPrefab; // Added at the end of the mountain
    [SerializeField] private GameObject firstSegment; // Gameobject in the scene
    [SerializeField] private float rightBound = 40;
    [SerializeField] private float leftBound = -40;

    List<GameObject> mountainObjects = new List<GameObject>();
    Vector2 anchor;
    Camera camera;
    // Start is called before the first frame update
    // Spawn the first segment
    void Start()
    {
        camera = Camera.main;
        // anchor = initialAnchor;
        SpriteRenderer firstRenderer;
        if (firstSegment != null && firstSegment.TryGetComponent<SpriteRenderer>(out firstRenderer))
            anchor = firstSegment.transform.position + firstRenderer.bounds.size / 2;
        else
        {
            Debug.LogError("First Segment has no Renderer");
            anchor = new Vector2(-20, -50);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(mountainObjects.Count);
        // Debug.Log(string.Format("Anchor: {0}", anchor));
        if (anchor.x - camera.transform.position.x < rightBound)
        {
            // spawn a random segment and move anchor
            Debug.Log("Spawning Mountain");
            
            GameObject g;
            if (Win)
            {
                g = Instantiate(winPrefab, anchor, Quaternion.identity);
            }
            else
            {
                int randomInd = Random.Range(0, mountainPrefabs.Count);
                g = Instantiate(mountainPrefabs[randomInd], anchor, Quaternion.identity);
            }
            mountainObjects.Add(g);

            GroundSegment segmentData;
            if (g.TryGetComponent<GroundSegment>(out segmentData))
            {
                Debug.Log(segmentData.SpriteOffset);
                Debug.Log(segmentData.AnchorOffset);
                g.transform.position += new Vector3(segmentData.SpriteOffset.x, segmentData.SpriteOffset.y, 0);
                anchor += segmentData.AnchorOffset;
            }

        }
        // delete the first (leftmost) object if too far left (offscreen)
        if (mountainObjects.Count > 0 && mountainObjects[0].transform.position.x - camera.transform.position.x < leftBound)
        {
            Debug.Log("Deleting Mountain");
            Destroy(mountainObjects[0]);
            mountainObjects.RemoveAt(0);
        }
    }
}
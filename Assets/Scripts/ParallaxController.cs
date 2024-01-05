using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public GameObject parallaxObject;
    [SerializeField] public Vector2 MovementScale;
    [SerializeField] public GameObject ReferenceObject;
    Vector2 refPrevPos;
    private Camera camera;
    private Vector2 cameraExtent;
    private Vector2 screenUR;
    private Vector2 screenBL;
    // private Vector2 offset;
    void Start()
    {
        camera = Camera.main;
        cameraExtent = new Vector2((camera.orthographicSize * camera.aspect)/2, camera.orthographicSize/2);
        refPrevPos = ReferenceObject.transform.position;
        // offset = transform.position - camera.transform.position;
    }

    void Update()
    {
        Vector2 refVel = (Vector2)(ReferenceObject.transform.position) - refPrevPos;
        screenUR = (Vector2)this.transform.position + cameraExtent;
        screenBL = (Vector2)this.transform.position - cameraExtent;
        Debug.Log(screenUR);
        Debug.Log(screenBL);
        MoveAndUpdate(refVel);
        refPrevPos = ReferenceObject.transform.position;
    }

    public void MoveAndUpdate(Vector2 movement)
    {
        // movement.x = 0;
        float parallaxLeft = float.MaxValue;
        float parallaxRight = float.MinValue;
        float parallaxY = -1;
        if (transform.childCount > 0 )
        {
            foreach (Transform child in transform)
            {
                SpriteRenderer childRenderer;
                child.position = (Vector2)child.position + Vector2.Scale(movement, -MovementScale);

                if (child.gameObject.TryGetComponent(out childRenderer))
                {
                    parallaxLeft = Mathf.Min(childRenderer.bounds.min.x, parallaxLeft);
                    parallaxRight = Mathf.Max(childRenderer.bounds.max.x, parallaxRight);
                    parallaxY = childRenderer.bounds.center.y;
                }
            }
            if (parallaxRight <= screenUR.x + 4)
            {
                GameObject rChild = Instantiate(parallaxObject, new Vector2(parallaxRight + parallaxObject.GetComponent<SpriteRenderer>().bounds.extents.x, parallaxY), Quaternion.identity);
                rChild.transform.parent = transform;
            }
            if (parallaxLeft >= screenBL.x - 4)
            {
                GameObject lChild = Instantiate(parallaxObject, new Vector2(parallaxLeft - parallaxObject.GetComponent<SpriteRenderer>().bounds.extents.x, parallaxY), Quaternion.identity);
                lChild.transform.parent = transform;
            }

        }
    }
}

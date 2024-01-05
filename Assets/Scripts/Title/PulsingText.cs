using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PulsingText : MonoBehaviour
{
    private RectTransform rectTransform;
    private bool isBouncingTitle = false;

    void Awake()
    {
        rectTransform = this.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(BounceTitle());
    }

    private IEnumerator BounceTitle()
    {
        if (!isBouncingTitle)
        {
            isBouncingTitle = true;

            //Vector3 sc = rectTransform.transform.localScale;
            float minY = rectTransform.anchorMin.y;
            float maxY = rectTransform.anchorMax.y;
            float minX = rectTransform.anchorMin.x;
            float maxX = rectTransform.anchorMax.x;
            for (float i = 1f; i <= 25f; i ++)
            {
                //rectTransform.localScale.Set(sc.x, sc.y + 0.01f, sc.z);
                rectTransform.anchorMin = new Vector2(rectTransform.anchorMin.x, minY-i/3000f);
                rectTransform.anchorMax = new Vector2(rectTransform.anchorMax.x, maxY+i/3000f);
                rectTransform.anchorMin = new Vector2(minX+i/4000f, rectTransform.anchorMin.y);
                rectTransform.anchorMax = new Vector2(maxX-i/4000f, rectTransform.anchorMax.y);
                yield return new WaitForSeconds(0.04f);
                //Debug.Log("Expanding");
            }
            for (float i = 25f; i > 0f; i--)
            {
                //rectTransform.transform.localScale.Set(sc.x, sc.y - 0.01f, sc.z);
                rectTransform.anchorMin = new Vector2(rectTransform.anchorMin.x, minY-i/3000f);
                rectTransform.anchorMax = new Vector2(rectTransform.anchorMax.x, maxY+i/3000f);
                rectTransform.anchorMin = new Vector2(minX+i/4000f, rectTransform.anchorMin.y);
                rectTransform.anchorMax = new Vector2(maxX-i/4000f, rectTransform.anchorMax.y);
                yield return new WaitForSeconds(0.04f);
                //Debug.Log("Contracting");
            }

            isBouncingTitle = false;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class UIAuroraLight : MonoBehaviour
{
    private Light2D auroraLights;
    private bool isChangingAuroraColor;
    
    void Awake()
    {
        try
        {
        auroraLights = GetComponent<Light2D>();
        }
        catch
        {
            Debug.Log("No Light2D Component found in this game object!");
        }
    }

    void Update()
    {
        if (auroraLights != null)
        {
            StartCoroutine(FluctuateAuroraLights());
        }
    }
    
    private IEnumerator FluctuateAuroraLights()
    {
        if (!isChangingAuroraColor)
        {
            isChangingAuroraColor = true;

            // Find the current HSV value of the aurora
            float h, s, v;
            Color.RGBToHSV(auroraLights.color, out h, out s, out v);

            // Choose a new hue for the aurora color
            //float rand_h_change = Random.Range(-50f, 50f);
            //float new_h = h + rand_h_change/360f;
            float rand_h_change = Random.Range(0f, 1f);
            float new_h = h + rand_h_change;

            if (new_h > 1)
            {
                new_h -= 1;
                rand_h_change -= 1;
            }
            /*
            else if (new_h < 0)
            {
                new_h += 1;
            }
            */

            // Lerp to the new color of the aurora
            for (float i = h; i < new_h; i += rand_h_change / 10f)
            {
                auroraLights.color = Color.HSVToRGB(i,s,v);
                yield return new WaitForSeconds(0.25f);
            }
            isChangingAuroraColor = false;
        }
    }
}

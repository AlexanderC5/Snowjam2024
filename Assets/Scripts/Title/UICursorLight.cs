using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class UICursorLight : MonoBehaviour
{
    private Light2D light2D;
    
    void Awake()
    {
        try
        {
            light2D = this.GetComponent<Light2D>();
        }
        catch
        {
            Debug.Log("No 'Light2D' component detected on this cursor");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (light2D == null) return;

        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.transform.localPosition = new Vector3(mousePos.x, mousePos.y, 0);
    }
}

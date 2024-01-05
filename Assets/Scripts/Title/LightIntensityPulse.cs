using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightIntensityPulse : MonoBehaviour
{
    bool isChangingIntensity = false;
    [SerializeField] float minIntensity;
    [SerializeField] float maxIntensity;
    [SerializeField] int numSteps;
    [SerializeField] float timePerStep;
    private new Light2D light;

    void Awake()
    {
        light = GetComponent<Light2D>();
    }

    // Start is called before the first frame update
    void Update()
    {
        StartCoroutine(Pulse());
    }

    private IEnumerator Pulse()
    {
        if (!isChangingIntensity)
        {
            isChangingIntensity = true;
            light.intensity = minIntensity;
            var stepSize = (maxIntensity - minIntensity) / numSteps;
            while (light.intensity < maxIntensity)
            {
                light.intensity += stepSize;
                yield return new WaitForSeconds(timePerStep);
            }while (light.intensity > minIntensity)
            {
                light.intensity -= stepSize;
                yield return new WaitForSeconds(timePerStep);
            }
            yield return new WaitForSeconds(1f); // pause 1 second between pulses
            isChangingIntensity = false;
        }
    }
}

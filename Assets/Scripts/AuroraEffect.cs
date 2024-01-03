using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuroraEffect : MonoBehaviour
{
    [SerializeField]    private float[] ZONE_ALTITUDE = {500f, 1000f, 1500f};
    [SerializeField]    private float[] AURORA_TRANSPARENCY = {90f, 18f, 4f};

    private GameObject player;
    private ParticleSystem[] particles;
    private float playerHeight;
    private float alphaMultiplier;
    private Gradient grad = new Gradient();
    private bool isChangingColor = false;
    private Vector3 colorMutation = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        particles = this.GetComponentsInChildren<ParticleSystem>();

        // Hard-coded default color gradient (so we don't accidentally lose the values)
        grad.SetKeys(new GradientColorKey[] {
            new GradientColorKey(new Color(0.35f, 0.80f, 0.61f), 0f),
            new GradientColorKey(new Color(0.56f, 0.77f, 0.76f), 0.5f),
            new GradientColorKey(new Color(0.20f, 0.33f, 0.68f), 1f)
        }, new GradientAlphaKey[] {
            new GradientAlphaKey(0f, 0f),
            new GradientAlphaKey(0.78f, 0.2f),
            new GradientAlphaKey(0.49f, 0.5f),
            new GradientAlphaKey(0.80f, 0.85f),
            new GradientAlphaKey(0f, 1f)
        });
    }

    // Update is called once per frame
    void Update()
    {
        playerHeight = player.transform.localPosition.y;
        if (playerHeight < 0) playerHeight = 0;
        if (playerHeight > ZONE_ALTITUDE[2]) playerHeight = ZONE_ALTITUDE[2];

        AuroraIntensity();
        AuroraColor();

        // For each of the aurora GameObjects, Stop emitting particles, update the color, then start emitting again.
        //   Particles can not be changed once they are already being emitted.
        for (int i = 0; i < particles.Length; i++)
        {
            particles[i].Stop();
            var m = particles[i].main;
            //m.startColor = new Color(m.startColor.color.r, m.startColor.color.g, m.startColor.color.b, AURORA_TRANSPARENCY[i]/255f * alphaMultiplier);
            m.startColor = new Color(1f + colorMutation.x, 1f + colorMutation.y, 1f + colorMutation.z, AURORA_TRANSPARENCY[i]/255f * alphaMultiplier);
            m.startSize = (float)((i + 1f) * (0.75 + 0.5 * playerHeight / ZONE_ALTITUDE[2])); // Length of aurora = position in array * inverse altitude modifier
            particles[i].Play();   
        }
    }

    /// <summary>
    /// Changes the Alpha values of the aurora based on the player's altitude
    /// </summary>
    private void AuroraIntensity()
    {
        // Determine alpha multipliers by lerping the player's altitude:
        if (playerHeight < ZONE_ALTITUDE[0])
        {
            alphaMultiplier = 0.1f * (playerHeight / ZONE_ALTITUDE[0]);
        }
        else if (playerHeight < ZONE_ALTITUDE[1])
        {
            alphaMultiplier = 0.1f + 0.4f * ((playerHeight - ZONE_ALTITUDE[0]) / (ZONE_ALTITUDE[1] - ZONE_ALTITUDE[0]));
        }
        else if (playerHeight < ZONE_ALTITUDE[2])
        {
            alphaMultiplier = 0.5f + 0.5f * ((playerHeight - ZONE_ALTITUDE[1]) / (ZONE_ALTITUDE[2] - ZONE_ALTITUDE[1]));
        }
        else
        {
            alphaMultiplier = 1f;
        }

        /*
        for (int i = 0; i < particles.Length; i++)
        {
            var aurora = particles[i].colorOverLifetime;
            var g = grad;
            for (int j = 0; j < g.alphaKeys.Length; j++)
            {
                g.alphaKeys[j].alpha *= alphaMultiplier;
                Debug.Log(j + " | " + g.alphaKeys[j].alpha * alphaMultiplier);
            }
        }
        */ 
    }

    /// <summary>
    /// Randomizes the color of the aurora slightly as time posses. Only one color modification can run at a time due to the Coroutine
    /// </summary>
    private void AuroraColor()
    {
        StartCoroutine(AuroraColoring());
    }

    private IEnumerator AuroraColoring()
    {
        if (!isChangingColor)
        {
            isChangingColor = true;
            //var colorToMutate = UnityEngine.Random.Range(0,3); // Modify parameter 0, 1, or 2
            //bool direction = (UnityEngine.Random.value > 0.5f); // Randomly increase or decrease
            float amtx = UnityEngine.Random.Range(0f,0.8f);
            float amty = UnityEngine.Random.Range(0f,0.8f);
            float amtz = UnityEngine.Random.Range(0f,0.8f);

            //if (!direction) amt = -1 * Mathf.Abs(amount); // 50% chance for negative

            for (float i = 0.1f; i <= 1f; i += 0.1f)
            {
                colorMutation.x = 1 - (amtx * i);
                colorMutation.y = 1 - (amty * i);
                colorMutation.z = 1 - (amtz * i);
                /*
                switch (colorToMutate)
                {
                    case 0:
                        colorMutation.x += amt * i;
                        break;
                    case 1:
                        colorMutation.y += amt * i;
                        break;
                    default:
                        colorMutation.z += amt * i;
                        break;
                }
                */
                yield return new WaitForSeconds(0.25f);
            }
            
            for (float i = 1f; i > 0f; i -= 0.1f)
            {
                colorMutation.x = 1 - (amtx * i);
                colorMutation.y = 1 - (amty * i);
                colorMutation.z = 1 - (amtz * i);
                /*
                switch (colorToMutate)
                {
                    case 0:
                        colorMutation.x += amt * i;
                        break;
                    case 1:
                        colorMutation.y += amt * i;
                        break;
                    default:
                        colorMutation.z += amt * i;
                        break;
                }
                */
                yield return new WaitForSeconds(0.25f);
            }

            colorMutation.Set(0f, 0f, 0f); // Reset the color mutator
            yield return new WaitForSeconds(1f);

            isChangingColor = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
//using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    private float FADE_DURATION = 0.75f; // Duration of the fade to/from black

    [SerializeField] GameObject blackOverlay;
    private Animator fadeAnimator;

    private bool isInTransition = false;
    private bool isOptionsMenuOpen = false;

    void Awake()
    {
        fadeAnimator = blackOverlay.GetComponent<Animator>();
    }

    public void returnToTitle()
    {
        StartCoroutine(fadeToBlack());
    }

    // s is a keyword that determines what happens after fading to black
    private IEnumerator fadeToBlack()
    {
        if (!isInTransition)
        {
            isInTransition = true;
            fadeAnimator.Play("FadeToBlack");
            yield return new WaitForSeconds(FADE_DURATION);
            isInTransition = false;

            SceneManager.LoadScene("Start");
        }
    }
}

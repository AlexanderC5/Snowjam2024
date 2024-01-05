using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private float FADE_DURATION = 0.75f; // Duration of the fade to/from black
    private float SLIDE_DURATION = 0.50f; // Duration of the options menu slide transition

    [SerializeField] GameObject blackOverlay;
    [SerializeField] GameObject slidingMenu;
    private Animator fadeAnimator;
    private Animator slideAnimator;

    private bool isInTransition = false;
    private bool isOptionsMenuOpen = false;

    void Awake()
    {
        fadeAnimator = blackOverlay.GetComponent<Animator>();
        slideAnimator = slidingMenu.GetComponent<Animator>();
    }

    void Update()
    {
        if (isOptionsMenuOpen && Input.GetMouseButtonDown(1))
        {
            toggleOptions();
        }
    }

    public void playGame()
    {
        StartCoroutine(fadeToBlack("play"));
    }

    public void toggleOptions()
    {
        StartCoroutine(ToggleOptionsMenu());
    }
    
    public void quitGame()
    {
        StartCoroutine(fadeToBlack("quit"));
    }

    // s is a keyword that determines what happens after fading to black
    private IEnumerator fadeToBlack(string s)
    {
        if (!isInTransition)
        {
            isInTransition = true;
            fadeAnimator.Play("FadeToBlack");
            slideAnimator.Play("SlideDown");
            yield return new WaitForSeconds(FADE_DURATION);
            isInTransition = false;
            switch(s)
            {
                case "play":
                    SceneManager.LoadScene("Main");
                    break;
                case "quit":
                    Application.Quit();
                    break;
                default:
                    Debug.Log("An invalid keyword was passed into FadeToBlack");
                    break;
            }
        }
    }

    private IEnumerator ToggleOptionsMenu()
    {
        if (!isInTransition)
        {
            isInTransition = true;
            if (!isOptionsMenuOpen) // Open the options menu by sliding left
            {
                slideAnimator.Play("SlideLeft");
                yield return new WaitForSeconds(SLIDE_DURATION);
                isOptionsMenuOpen = true;
            }
            else // Close the options menu by sliding right
            {
                slideAnimator.Play("SlideRight");
                yield return new WaitForSeconds(SLIDE_DURATION);
                isOptionsMenuOpen = false;
            }
            isInTransition = false;
        }
    }
}

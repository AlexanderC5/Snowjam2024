using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class SwitchSpriteWhenClicked : MonoBehaviour
{
    [SerializeField] List<Sprite> spritesList = new List<Sprite>();
    [SerializeField] bool isRandomizedOnStart = true;

    void Start()
    {
        if (isRandomizedOnStart)
        {
            ChangeSprite();
        }
    }

    public void ChangeSprite()
    {
        if (spritesList.Count < 1) return;

        int randSprite = Random.Range(0,spritesList.Count);
        this.GetComponent<UnityEngine.UI.Image>().sprite = spritesList[randSprite];
    }
}

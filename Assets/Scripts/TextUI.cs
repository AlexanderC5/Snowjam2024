using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextUI : MonoBehaviour
{
    public TMP_Text canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas.text = "Score: " + GameObject.FindWithTag("Player").GetComponent<Player>().score + "\nDistance: " + (int) GameObject.FindWithTag("Player").GetComponent<Player>().distance + "\nHeight: " + (int) GameObject.FindWithTag("Player").GetComponent<Player>().height;
    }

    // Update is called once per frame
    void Update()
    {
        canvas.text = "Score: " + GameObject.FindWithTag("Player").GetComponent<Player>().score + "\nDistance: " + (int)GameObject.FindWithTag("Player").GetComponent<Player>().distance + "\nHeight: " + (int)GameObject.FindWithTag("Player").GetComponent<Player>().height;
    }
}

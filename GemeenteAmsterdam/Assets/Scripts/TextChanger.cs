using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Mime;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class TextChanger : MonoBehaviour
{
    private Text myTextObject; // Assign this in the inspector

    private void Start()
    {
        myTextObject = GetComponent<Text>();
    }

    public void UpdateText(string newText)
    {
        myTextObject.text = newText;
    }

    public void ClearText()
    {
        myTextObject.text = "";
    }

    public void SetTextSize(int size)
    {
        myTextObject.fontSize = size;
    }
}

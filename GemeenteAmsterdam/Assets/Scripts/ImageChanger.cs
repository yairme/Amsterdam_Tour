using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class ImageChanger : MonoBehaviour
{
    [SerializeField] private GameObject myImageObject; // Assign this in the inspector

    public void ChangeImageSprite(Sprite newSprite)
    {
        myImageObject.GetComponent<Image>().sprite = newSprite;
    }

    public void ChangeImageSize(Vector2 newSize)
    {
        //myImageObject.rectTransform.sizeDelta = newSize;
        //myImageObject.GetComponent<SpriteRenderer>().size = newSize;
    }

    public void ChangeImagePosition(Vector2 newPosition)
    {
        //myImageObject.rectTransform.anchoredPosition = newPosition;
        //myImageObject.GetComponent<SpriteRenderer>(). = newPosition;
    }
}

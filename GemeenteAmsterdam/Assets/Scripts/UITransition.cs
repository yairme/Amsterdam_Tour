using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITransition : MonoBehaviour
{
    public GameObject menuUI;

    public void ShowMenu()
    {
        menuUI.SetActive(true);
    }
        
    public void GoBack()
    {
        menuUI.SetActive(false);
    }
}
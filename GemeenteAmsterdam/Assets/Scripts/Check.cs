using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Check : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    private void Start()
    {
        text.text = "kut";
#if UNITY_EDITOR
        text.text = "windows";
#elif UNITY_ANDROID
        text.text = "android";
#endif
    }
}

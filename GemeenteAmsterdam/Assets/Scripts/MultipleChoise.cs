using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.UIElements;
using System.Diagnostics;
using UnityEditor.Presets;
using System.Linq;
using static UnityEditor.FilePathAttribute;

[Serializable]
struct Options
{
    public string ButText;
    public bool check;
}
[CreateAssetMenu(menuName = "ScriptableObjects/MultipleChoice")]
public class MultipleChoise : Queations
{
    [SerializeField] private Options[] Options;
    private Button[] _Buttons;
    public override QuestionType GetTyping()
    {
        type = QuestionType.MultipleChoice;
        return base.GetTyping();
    }
    public override void SetButtons(GameObject canvas, LocationQuestions Location)
    {
        _Buttons = canvas.GetComponentsInChildren<Button>();
        for (int i = 0; i < Options.Length; i++)
        {
            _Buttons[i].gameObject.SetActive(true);
            _Buttons[i].GetComponentInChildren<TMP_Text>().text = Options[i].ButText;
            _Buttons[i].onClick.RemoveAllListeners();
            if (Options[i].check)
            {
                _Buttons[i].onClick.AddListener(() => { Location.Awnser(type, Points); });
            }
            else
            {
                _Buttons[i].onClick.AddListener(() => { Location.Awnser(type); });
            }
        }
        for (int i = Options.Length; i < _Buttons.Length; i++)
        {
            _Buttons[i].gameObject.SetActive(false);
        }
    }
    //public void SetBut(Button []_Buttons, LocationQuestions Location)
    //{
    //    for ( int i = 0 ; i < Options.Length; i++)
    //    {
    //        _Buttons[i].gameObject.SetActive(true);
    //        _Buttons[i].GetComponentInChildren<TMP_Text>().text = Options[i].ButText;
    //        _Buttons[i].onClick.RemoveAllListeners();
    //        if (Options[i].check)
    //        {
    //            _Buttons[i].onClick.AddListener(() => { Location.Awnser(type, Points); });
    //        }
    //        else
    //        {
    //            _Buttons[i].onClick.AddListener(() => { Location.Awnser(type); });
    //        }
    //    } 
    //    for  ( int i = Options.Length; i < _Buttons.Length; i++)
    //    {
    //        _Buttons[i].gameObject.SetActive(false);
    //    }
    //}
}

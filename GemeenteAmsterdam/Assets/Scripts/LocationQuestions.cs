using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LocationQuestions : MonoBehaviour
{
    [SerializeField] private Queations[] Queations;
    [SerializeField] private GameObject Multiple, Slide;

    private int Index, Points;

    private void Start()
    {
        StartShow();
    }
    private void StartShow()
    {
        if (Index == Queations.Length)
        {
            PlayerPrefs.SetInt("Poins", PlayerPrefs.GetInt("Points") + Points);
            return;
        }

        switch (Queations[Index].GetTyping())
        {
            case QuestionType.MultipleChoice:
                Queations[Index].SetButtons(Multiple, this);
                break;
            case QuestionType.Slider:
                Queations[Index].SetButtons(Slide, this);
                break;
        }
        Index++;
    }
    public void Awnser(QuestionType type, int _Points = 0, string Info = null)
    {
        Points += _Points;
        switch (type)
        {
            case QuestionType.MultipleChoice:
                Multiple.SetActive(false);
                break;
            case QuestionType.Slider:
                Slide.SetActive(false);
                break;
        }
        StartShow();
    }
}

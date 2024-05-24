using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

[CreateAssetMenu(menuName = "ScriptableObjects/Slider")]
public class Sliding : Queations
{
    [SerializeField] private int MinValue, MaxValue, CorrectVlue, Jump, NegativePoints;
    private TMP_Text text;
    private Slider slider;
    private int CorrectSteps;
    public override QuestionType GetTyping()
    {
        type = QuestionType.Slider;
        return base.GetTyping();
    }
    public override void SetButtons(GameObject canvas, LocationQuestions Location)
    {
        slider = canvas.GetComponent<Slider>();
        text = slider.GetComponentInChildren<TMP_Text>();

        CorrectSteps = (CorrectVlue - MinValue) / Jump;
        slider.maxValue = (MaxValue - MinValue) / Jump;

        OnValueChange();
        slider.onValueChanged.RemoveAllListeners();
        slider.onValueChanged.AddListener(delegate { OnValueChange(); });

        slider.GetComponentInChildren<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
        slider.GetComponentInChildren<UnityEngine.UI.Button>().onClick.AddListener(delegate { Location.Awnser(type, PointsCalculation()); });
    }
    private void OnValueChange()
    {
        text.text = ((int)slider.value * Jump + MinValue).ToString();
    }
    private int PointsCalculation()
    {
        int temp = Points - (Mathf.Abs(CorrectSteps - (int)slider.value) * NegativePoints);
        if (temp > 0)
        {
            return temp;
        }
        return 0;
    }
}

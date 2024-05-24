using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public enum QuestionType
{
    MultipleChoice,
    Slider,
}
public class Queations : ScriptableObject
{
    [SerializeField] protected string Question, Info, Hint;
    [SerializeField] protected int Points = 100;
    protected QuestionType type;
    
    public virtual QuestionType GetTyping()
    {
        return type;
    }
    public virtual void SetButtons(GameObject canvas,LocationQuestions Location)
    {

    }
}

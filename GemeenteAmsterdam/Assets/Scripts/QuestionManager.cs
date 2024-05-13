using UnityEngine;
using TMPro;

public class QuestionManager : MonoBehaviour
{
    public TMP_Text questionText; 
    public TMP_Text[] answerTexts; 
    public string[] questions; 
    
    private string[][] _answers; 
    private int _currentQuestionIndex = 0; 

    void Start()
    {
        DisplayQuestion(); 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextQuestion();
        }
    }

    void DisplayQuestion()
    {
        
        if (_currentQuestionIndex < questions.Length)
        {
            
            questionText.text = questions[_currentQuestionIndex];

            
            for (int i = 0; i < answerTexts.Length; i++)
            {
                answerTexts[i].text = _answers[_currentQuestionIndex][i];
            }
        }
        else
        {
            
            questionText.text = "No more questions!";
            foreach (var answerText in answerTexts)
            {
                answerText.text = "";
            }
        }
    }

    public void NextQuestion()
    {
        _currentQuestionIndex++;
        
        DisplayQuestion();
    }
}
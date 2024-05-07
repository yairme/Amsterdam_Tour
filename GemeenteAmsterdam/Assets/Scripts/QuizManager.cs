using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;


public class QuizManager : MonoBehaviour
{
    public List<QuestionSystem> questionList;
    public GameObject[] options;
    public int currentQuestion;
    public Text questionText;

    private void Start()
    {
        generateQuestion();
    }

    void SetAnswers()
    {
        for (int i = 0; i < options.Length; i++)
        {
            //options[i].transform.GetChild(0).GetComponent<Text>().text = questionList[currentQuestion].answers[i];
        }
    }

    void generateQuestion()
    {
        currentQuestion = Random.Range(0, questionList.Count);

        questionText.text = questionList[currentQuestion].question;
    }
}

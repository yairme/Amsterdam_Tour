using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    struct QuestionObject
    {
        public string question;
        public Sprite image;
        public string[] options;
        public float minValue;
        public float maxValue;
        public bool[] check;
        public float scoreBoard;
        public float addedPoints;
    }
    /// <summary>
    ///  Sets the canvas active or inactive.
    /// </summary>
    /// <param name="_canvas">The canvas game object.</param> 
    /// <param name="_active">A boolean value to set the canvas active or inactive.</param> 
    private void SetCanvasActive(GameObject _canvas, bool _active)
    {
        _canvas.SetActive(_active);
    }
    
    /// <summary>
    /// Sets up a multiple choice question on the canvas.
    /// </summary>
    /// <param name="_canvas">The canvas game object.</param>
    /// <param name="_buttons">An array of button game objects representing the answer options.</param>
    /// <param name="_image">The image game object to display the question image.</param>
    /// <param name="_questionText">The text game object to display the question text.</param>
    /// <param name="_questionObject">The question object containing the question and answer options.</param>
    public void MultipleChoice(GameObject _canvas, GameObject[] _buttons, GameObject _image, GameObject _questionText)
    {
        
        var _questionObject = new QuestionObject();
        // Set the question image and text
        _image.GetComponent<Image>().sprite = _questionObject.image;
        _questionText.GetComponent<Text>().text = _questionObject.question;
        
        // Set up the answer buttons
        for(int i = 0; i < _questionObject.options.Length; i++)
        {
            var buttonText = _buttons[i].GetComponentInChildren<Text>();
            _buttons[i].SetActive(true);
            buttonText.text = _questionObject.options[i];
            if(_questionObject.check[i])
            {
                _buttons[i].GetComponent<Button>().onClick.AddListener(() => Debug.Log("Correct Answer"));
            }
            else
            {
                _buttons[i].GetComponent<Button>().onClick.AddListener(() => Debug.Log("Wrong Answer"));
            }
        }
        
        // Activate the necessary game objects
        _image.SetActive(true);
        _questionText.SetActive(true);
        _canvas.SetActive(true);
    }
    
    /// <summary>
    /// Sets up a slider question on the canvas.
    /// </summary>
    /// <param name="_canvas">The canvas game object.</param>
    /// <param name="_questionText">The text game object to display the question text.</param>
    /// <param name="_counter">The text game object to display the current slider value.</param>
    /// <param name="_slider">The slider game object to select a value.</param>
    /// <param name="_questionObject">The question object containing the question and slider properties.</param>
    public void Slider(GameObject _canvas, GameObject _questionText, GameObject _counter, GameObject _slider , GameObject _button)
    {
        var _questionObject = new QuestionObject();
        // Set the question text
        _questionText.GetComponent<Text>().text = _questionObject.question;
        
        // Set the slider properties
        _slider.GetComponent<Slider>().minValue = _questionObject.minValue;
        _slider.GetComponent<Slider>().maxValue = _questionObject.maxValue;
        _counter.GetComponent<Text>().text = _slider.GetComponent<Slider>().value.ToString();
        _button.GetComponent<Button>().onClick.RemoveAllListeners();
        for (int i = 0; i < _questionObject.options.Length; i++)
        {
            _button.GetComponent<Button>().onClick.AddListener(() =>
            {
                float sliderValue = _slider.GetComponent<Slider>().value;
                float distance = Mathf.Abs(sliderValue - _questionObject.options[i].Length);
                float maxDistance = Mathf.Abs(_questionObject.maxValue - _questionObject.minValue);
                
                if (_questionObject.check[i])
                {
                    _questionObject.scoreBoard = _questionObject.addedPoints;
                }
                else
                {
                    _questionObject.scoreBoard = _questionObject.addedPoints - (distance / maxDistance);
                }
                
                Debug.Log("Score: " + _questionObject.scoreBoard);
            });
        }

        // Activate the necessary game objects
        _questionText.SetActive(true);
        _canvas.SetActive(true);
    }

    /// Opens a canvas and updates the text based on the addedPoints value.
    /// </summary>
    /// <param name="_canvas">The canvas game object.</param>
    /// <param name="_text">The text game object to update.</param>
    /// <param name="_addedPoints">The value to update the text with.</param>
    /// <param name="_button">The button game object to modify its behavior.</param>
    public void OpenCanvas(GameObject _canvas, GameObject _text, GameObject _button)
    {
        var _questionObject = new QuestionObject();
        // Set the text based on the addedPoints value
        _text.GetComponent<Text>().text = _questionObject.addedPoints.ToString();
        
        // Modify the button behavior
        _button.GetComponent<Button>().onClick.RemoveAllListeners();
        _button.GetComponent<Button>().onClick.AddListener(() => Debug.Log("Button Clicked"));
        
        // Activate the necessary game objects
        _text.SetActive(true);
        _canvas.SetActive(true);
    }

}

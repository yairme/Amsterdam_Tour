using System;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
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
    public void MultipleChoice(GameObject _canvas, GameObject[] _buttons, GameObject _image, GameObject _questionText, GameObject _questionObject)
    {
        // Set the question image and text
        _image.GetComponent<Image>().sprite = _questionObject.GetStruct().image;
        _questionText.GetComponent<Text>().text = _questionObject.GetStruct().question;
        
        // Set up the answer buttons
        for(int i = 0; i < _questionObject.GetStruct().options.length; i++)
        {
            var buttonText = _buttons[i].GetComponentInChildren<Text>();
            _buttons[i].SetActive(true);
            buttonText.text = _questionObject.GetStruct().options[i].text;
            _buttons[i].GetComponent<Button>().onClick.AddListener(_questionObject.GetStruct().check);
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
    public void Slider(GameObject _canvas, GameObject _questionText, GameObject _counter, GameObject _slider, GameObject _questionObject)
    {
        // Set the question text
        _questionText.GetComponent<Text>().text = _questionObject.GetStruct().question;
        
        // Set the slider properties
        _slider.GetComponent<Slider>().minValue = _questionObject.GetStruct().minValue;
        _slider.GetComponent<Slider>().maxValue = _questionObject.GetStruct().maxValue;
        _counter.GetComponent<Text>().text = _slider.GetComponent<Slider>().value.ToString();
        _slider.GetComponent<Slider>().onValueChanged.AddListener(_questionObject.GetStruct().check);

        // Activate the necessary game objects
        _questionText.SetActive(true);
        _canvas.SetActive(true);
    }
}

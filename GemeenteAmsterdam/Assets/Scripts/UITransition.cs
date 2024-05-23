using UnityEngine;
using UnityEngine.UI;

public class UITransition : MonoBehaviour
{
    [SerializeField] private GameObject[] Layouts;
    public void SetLayoutActive(string _layoutName)
    {
        foreach (GameObject Layout in Layouts)
        {
            if (Layout.name.ToLower() == _layoutName.ToLower()) 
            {
                Layout.SetActive(true);
            }
            if (Layout.name.ToLower() != _layoutName.ToLower())
            {
                Layout.SetActive(false);
            }
        }
    }

    public void ToggleUIElement(GameObject _UiElement)
    {
        bool isActive = _UiElement.activeInHierarchy;

        _UiElement.SetActive(!_UiElement.activeInHierarchy);
    }
}
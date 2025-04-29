using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextBoxManager : MonoBehaviour
{
    public RectTransform textBox;
    public TextMeshProUGUI activeTextInBox;
    public GameObject textBoxGroup;

    public void ChangeTextInBox(string newText)
    {
        activeTextInBox.text = newText;
    }

    public void CloseTextBox()
    {
        textBoxGroup.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void DisplayText(string textToDisplay)
    {
        Time.timeScale = 0f;
        ChangeTextInBox(textToDisplay);
        textBoxGroup.SetActive(true);
    }
}

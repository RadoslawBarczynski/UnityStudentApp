using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestPanelObjectVariables : MonoBehaviour
{
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI correctAnswer;
    public TextMeshProUGUI inCorrectAnswer1;
    public TextMeshProUGUI inCorrectAnswer2;


    public void ContentSetup(string result, string correct, string incorrect1, string incorrect2)
    {
        resultText.text = result;
        correctAnswer.text = correct; 
        inCorrectAnswer1.text = incorrect1;
        inCorrectAnswer2.text = incorrect2;
    }
}

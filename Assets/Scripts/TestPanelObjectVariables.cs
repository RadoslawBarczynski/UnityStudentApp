using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestPanelObjectVariables : MonoBehaviour
{
    public TestManager testManager;

    public GameObject ButtonParent;
    public List<GameObject> Buttons;

    public TextMeshProUGUI resultText;
    public TextMeshProUGUI correctAnswer;
    public TextMeshProUGUI inCorrectAnswer1;
    public TextMeshProUGUI inCorrectAnswer2;

    private void Start()
    {
        testManager = GameObject.FindGameObjectWithTag("TestManager").GetComponent<TestManager>();
        ShuffleButton(Buttons);
    }

    public void ShuffleButton(List<GameObject> list)
    {
        int random = Random.Range(0, 3);

        for(int i = 0; i < list.Count; i++)
        {
            if (random + 1 < list.Count-1)
            {
                GameObject temp = list[random];
                list[random] = list[random + 1];
                list[random + 1] = temp;

                list[random].transform.SetAsFirstSibling();
            }
            else
            {
                GameObject temp = list[random];
                list[random] = list[random - 1];
                list[random - 1] = temp;

                list[random].transform.SetAsFirstSibling();
            }
        }
    }

    public void AnswerButton(int value)
    {
        if (value == 1)
        {
            testManager.testPoints++;
        }
    }
    public void ContentSetup(string result, string correct, string incorrect1, string incorrect2)
    {
        resultText.text = result;
        correctAnswer.text = correct; 
        inCorrectAnswer1.text = incorrect1;
        inCorrectAnswer2.text = incorrect2;
    }
}

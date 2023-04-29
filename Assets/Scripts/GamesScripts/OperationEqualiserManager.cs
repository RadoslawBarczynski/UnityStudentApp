using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OperationEqualiserManager : MonoBehaviour
{
    //ui elements
    public Sprite GreenBackground, RedBackground;
    public List<TextMeshProUGUI> OperationTextList = new List<TextMeshProUGUI>();
    public List<TextMeshProUGUI> AnswerTextList = new List<TextMeshProUGUI>();
    public TextMeshProUGUI stageText, endText;
    public GameObject EndGamePanel;

    //variables
    public int points = 0, stage, correctIndex;

    // Start is called before the first frame update
    void Start()
    {
        stage = 1;
        GenerateOperation();
    }

    // Update is called once per frame
    void Update()
    {
        stageText.text = "Etap " + stage + "/5";
    }

    void GenerateOperation()
    {
        //multipliers
        int x1 = Random.Range(2, 10);
        int x2 = Random.Range(2, 10);
        //final result
        int result = x1 * x2;
        int fakeMultiplier = Random.Range(0, 10);
        int fakeMultiplier2 = Random.Range(0, 10);

        int indexO = Random.Range(0, 3); //incorrect operation
        int indexA = Random.Range(0, 3); //correct answer

        correctIndex = indexA;

        Debug.Log("x1 " + x1);
        Debug.Log("x2 " + x2);
        Debug.Log("result " + result);

        for (int i = 0; i < 3; i++)
        {
            if (i == indexO) //place where incorrect result is
            {
                OperationTextList[i].text = x1.ToString();
                OperationTextList[i].color = Color.red;
                OperationTextList[i].transform.parent.GetComponent<Image>().sprite = RedBackground;
            }
            else //place where correct result is
            {
                OperationTextList[i].text = result.ToString();
                OperationTextList[i].color = Color.green;
                OperationTextList[i].transform.parent.GetComponent<Image>().sprite = GreenBackground;
            }
        }

        bool firstActive = false;

        for (int i = 0; i < 3; i++)
        {           
            if (i == indexA)
            {
                AnswerTextList[i].text = x2.ToString();
            }
            else if (firstActive)
            {
                while (fakeMultiplier2 == x2 || fakeMultiplier2 == fakeMultiplier) //fake multiplier cant be the same as correct answer
                {
                    fakeMultiplier2 = Random.Range(0, 10);
                }
                AnswerTextList[i].text = fakeMultiplier2.ToString();
            }
            else if(!firstActive)
            {
                while (fakeMultiplier == x2) //fake multiplier cant be the same as correct answer
                {
                    fakeMultiplier = Random.Range(0, 10);
                }
                AnswerTextList[i].text = fakeMultiplier.ToString();

                firstActive = true;
            }
        }

    }

    public void ButtonLogic(int value)
    {
        if(value == correctIndex)
        {
            points++;
            stage++;
        }
        else if (stage == 5)
        {
            setEndText();
            EndGamePanel.SetActive(true);
            return;
        }
        else
        {
            stage++;
        }
        GenerateOperation();
        setEndText();
    }

    public void setEndText()
    {
        endText.text = "Rozwiazales poprawnie " + points + "/5 dzialan";
    }
}

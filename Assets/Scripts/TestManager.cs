using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    //variables
    private Guid activeTestId;
    private int i = 0; //current iteration
    private int j = 0; //max questions
    public int testPoints = 0;

    //components
    private UserDataLogged userDataLogged;
    private GameManager gameManager;

    //Ui Elements
    public GameObject TestPanelObject;
    public GameObject PanelsParent;
    public GameObject StartPanel;
    public GameObject EndPanel;

    public TextMeshProUGUI EndPanelText;
    public TextMeshProUGUI PassText;
    [SerializeField] 
    public List<GameObject> questionsPanels = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        userDataLogged = GameObject.Find("GameManager").GetComponent<UserDataLogged>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        FindActiveTestToSet();
        GenerateTestPanelsForEachQuestion();
    }

    public void SetPoint()
    {
        EndPanelText.text = "Miales " +  testPoints + "/" + questionsPanels.Count + " poprawnych odpowiedzi!";
        isPassed();
    }

    public void isPassed()
    {
        float result = Mathf.InverseLerp(0, questionsPanels.Count, testPoints);

        if(result * 100 >= 50f)
        {
            PassText.text = "Zdane!";
            PassText.color = Color.green;
            Debug.Log("Zdane!");
        }
        else
        {
            PassText.text = "Niezdane!";
            PassText.color = Color.red;
            Debug.Log("Niezdane!");
        }
    }

    void FindActiveTestToSet()
    {
        foreach(var test in userDataLogged.tests)
        {
            if (test.isActive)
            {
                activeTestId = test.id;
                Debug.Log(activeTestId);
                break;
            }
        }
    }

    void GenerateTestPanelsForEachQuestion()
    {
        foreach(var testQuestion in userDataLogged.testquestions)
        {
            if (testQuestion.TestId == activeTestId)
            {
                Question questionFound = userDataLogged.questions.Find(x => x.id == testQuestion.QuestionId);

                Debug.Log(questionFound.id);

                //-96 to center the object
                GameObject clone = Instantiate(TestPanelObject, new Vector3(transform.position.x - 96, transform.position.y, transform.position.z), transform.rotation, PanelsParent.transform);
                TestPanelObjectVariables testPanelObjectVariables = clone.GetComponent<TestPanelObjectVariables>();

                testPanelObjectVariables.ContentSetup(questionFound.operation, questionFound.answer1.ToString(), questionFound.answer2.ToString(), questionFound.answer3.ToString());
                clone.SetActive(false);

                questionsPanels.Add(clone);

            }
        }
        j = questionsPanels.Count;
    }

    public void NextQuestionButtonLogic()
    {
        if(i + 1 < j + 1)
        {
            if(i == 0)
            {
                StartPanel.SetActive(false);
                questionsPanels[i].SetActive(true);
                i++;
            }
            else
            {
                questionsPanels[i].SetActive(true);
                questionsPanels[i - 1].SetActive(false);
                i++;
            }
        }
        else if(i == j)
        {
            questionsPanels[i-1].SetActive(false);
            EndPanel.SetActive(true);
        }
    }
}

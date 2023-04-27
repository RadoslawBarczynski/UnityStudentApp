using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Supabase.Functions.Responses;
using Supabase.Storage;
using Supabase.Gotrue.Responses;
using Supabase.Realtime.Converters;
using System;
using Supabase.Realtime;
using static Supabase.Client;
using static UnityEditor.Progress;
using System.Threading.Tasks;
using UnityEngine.UI;
using TMPro;
using Assets.Scripts.Models;

public class LoginScript : MonoBehaviour
{
    string url;
    string key;
    public Supabase.Client client;

    //DB models
    public List<Student> students = new List<Student>();
    public List<Grade> grades = new List<Grade>();
    public List<Question> questions = new List<Question>();
    public List<Test> tests = new List<Test>();
    public List<TestQuestion> testquestions = new List<TestQuestion>();

    //components
    [SerializeField] GameManager gameManager;
    [SerializeField] UserDataLogged userDataLogged;
    [SerializeField] PanelSwitching panelSwitching;

    //ui elements
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public Button loginButton;
    public GameObject LoginPanel;

    //ui elements for main menu text setup
    public TextMeshProUGUI usernameText;
    public TextMeshProUGUI scoreText;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        userDataLogged = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UserDataLogged>();
        panelSwitching = GameObject.FindGameObjectWithTag("PanelManager").GetComponent<PanelSwitching>();
        Main();
        if (userDataLogged.isLoggedIn == true)
        {
            usernameText.text = "Czesc, " + userDataLogged.Username + "!";
            scoreText.text = "Masz " + userDataLogged.Score + " punktow!";
            panelSwitching.ChangePanelFunction(0);
            panelSwitching.ChangePanelFunction(1);
        }
        //Main2();
    }
    
    public void Login()
    {
        foreach(var student in students)
        {
            if(usernameInput.text == student.StudentLogin && passwordInput.text == student.StudentPassword)
            {
                userDataLogged.Username = student.StudentFirstName;
                userDataLogged.UserID = student.id;
                foreach (var grade in grades)
                {
                    if(grade.GradeId == student.GradeId)
                    {                       
                        userDataLogged.GradeID = grade.GradeId;
                        userDataLogged.Score = grade.Score;
                        //main menu text setup
                        usernameText.text = "Czesc, " + userDataLogged.Username + "!";
                        scoreText.text = "Masz " + userDataLogged.Score + " punktow!";
                        userDataLogged.isLoggedIn = true;
                        panelSwitching.ChangePanelFunction(0);
                        panelSwitching.ChangePanelFunction(1);
                        return;
                    }
                }             
            }
        }
    }

    public void DbForUserSetup()
    {
        userDataLogged.questions = questions;
        userDataLogged.students = students;
        userDataLogged.testquestions = testquestions;
        userDataLogged.tests = tests;
        userDataLogged.grades = grades;
    }

    public async void Main()
    {
        url = "https://melfibfnkmadpskpvist.supabase.co";
        key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Im1lbGZpYmZua21hZHBza3B2aXN0Iiwicm9sZSI6ImFub24iLCJpYXQiOjE2NzgxOTIxMTcsImV4cCI6MTk5Mzc2ODExN30.Kd7kp1eiKHzcTsg7noH02E_smiAJR_Y-9kR45wg6UlE";

        client = await Supabase.Client.InitializeAsync(url, key, new Supabase.SupabaseOptions
        {
            AutoConnectRealtime = true
        });

        //students table
        var result = await client.From<Student>().Get();
        students = result.Models;

        //grades table
        var result2 = await client.From<Grade>().Get();
        grades = result2.Models;

        //test table
        var result3 = await client.From<Question>().Get();
        questions = result3.Models;

        //question table
        var result4 = await client.From<Test>().Get();
        tests = result4.Models;

        //testquestion table
        var result5 = await client.From<TestQuestion>().Get();
        testquestions = result5.Models;


        DbForUserSetup();
    }


}

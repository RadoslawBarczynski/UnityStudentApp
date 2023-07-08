using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static Supabase.Client;

public class GameManager : MonoBehaviour
{
    //playerprefs last login
    private const string LastLoginKey = "LastLogin";
    private const int LoggedInToday = 0;
    private const int NotLoggedInToday = 1;
    //db
    string url;
    string key;
    public Supabase.Client client;

    //audio
    public AudioSource audioSound;
    public AudioClip[] sounds;

    //gameobjects
    public GameObject HomeworkParent;
    
    //prefabs
    public GameObject HomeworkElement;

    //components
    public UserDataLogged userDataLogged;
    public HomeworkController homeworkController;

    public void SpawnHomework()
    {
        homeworkController = GameObject.FindGameObjectWithTag("HomeworkManager").GetComponent<HomeworkController>();
        HomeworkParent = GameObject.FindGameObjectWithTag("HomeworkParent");
        
        foreach(var element in homeworkController.childrens)
        {
            Destroy(element.gameObject);
        }

        foreach (var homework in userDataLogged.homeworks)
        {
            if (homework.isActive && !userDataLogged.newGuids.Contains(homework.id))
            {
                 GenerateClone(homework);
            }
            Debug.Log(homework.id);                  
        }
        
    }

    public void GenerateClone(Homework homework)
    {
        GameObject clone = Instantiate(HomeworkElement, HomeworkParent.transform);
        clone.GetComponent<HomeworkSet>().numberOfGame = homework.GameNumber;
        clone.GetComponent<HomeworkSet>().SetTextFields(homework.ScoreToGet);
        clone.GetComponent<HomeworkSet>().homeworkId = homework.id;

        homeworkController.childrens.Add(clone);
    }

    public void CheckTasks(int gameNumber, int points)
    {
        foreach(var element in userDataLogged.homeworks)
        {
            if(element.isActive == true && element.GameNumber == gameNumber && element.ScoreToGet < points)
            {
                userDataLogged.newGuids.Add(element.id);
                Debug.Log(element.id.ToString());
                return;
            }
        }
    }

    public void TestScoreAdd()
    {
        Guid id = new Guid("7217e7cc-c37c-48f7-ba5e-0f9892a69c90");

        UpdateScore(5, id);
    }

    public async void UpdateScore(int scoreToAdd, Guid gradeId)
    {
        url = "https://melfibfnkmadpskpvist.supabase.co";
        key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Im1lbGZpYmZua21hZHBza3B2aXN0Iiwicm9sZSI6ImFub24iLCJpYXQiOjE2NzgxOTIxMTcsImV4cCI6MTk5Mzc2ODExN30.Kd7kp1eiKHzcTsg7noH02E_smiAJR_Y-9kR45wg6UlE";

        client = await Supabase.Client.InitializeAsync(url, key, new Supabase.SupabaseOptions
        {
            AutoConnectRealtime = true
        });

        //grades table
        var grades = await client.From<Grade>().Get();
        List<Grade> tempGrades = grades.Models;

        foreach (var grado in tempGrades)
        {
            if(grado.GradeId == gradeId)
            {
                grado.Score += scoreToAdd;
                await grado.Update<Grade>();
            }
        }

        userDataLogged.Score += scoreToAdd;
    }

    public async void InsertScore(Logs logs)
    {
        url = "https://melfibfnkmadpskpvist.supabase.co";
        key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Im1lbGZpYmZua21hZHBza3B2aXN0Iiwicm9sZSI6ImFub24iLCJpYXQiOjE2NzgxOTIxMTcsImV4cCI6MTk5Mzc2ODExN30.Kd7kp1eiKHzcTsg7noH02E_smiAJR_Y-9kR45wg6UlE";

        client = await Supabase.Client.InitializeAsync(url, key, new Supabase.SupabaseOptions
        {
            AutoConnectRealtime = true
        });

        //grades table
        await client.From<Logs>().Insert(logs);

    }

    public void checkLastLogin()
    {
        DateTime currentDate = DateTime.Now.Date;
        int i = PlayerPrefs.GetInt(LastLoginKey, NotLoggedInToday);

        // SprawdŸ, czy u¿ytkownik zalogowa³ siê dzisiaj
        if (i == LoggedInToday && currentDate == GetSavedLoginDate())
        {
            Debug.Log("U¿ytkownik zalogowa³ siê dzisiaj.");
        }
        else if (i == LoggedInToday && currentDate != GetSavedLoginDate())
        {
            userDataLogged.isLoggedToday = NotLoggedInToday;
            i = NotLoggedInToday;
        }
        else
        {
            Debug.Log("U¿ytkownik nie zalogowa³ siê dzisiaj.");
            userDataLogged.isLoggedToday = i;
            i = LoggedInToday;
        }

        // Zapisz aktualn¹ datê jako ostatnie logowanie
        PlayerPrefs.SetInt(LastLoginKey, i);
        SaveLoginDate(currentDate);
        PlayerPrefs.Save();
    }

    private DateTime GetSavedLoginDate()
    {
        string savedDate = PlayerPrefs.GetString(LastLoginKey + "_Date");
        return DateTime.Parse(savedDate);
    }

    private void SaveLoginDate(DateTime date)
    {
        PlayerPrefs.SetString(LastLoginKey + "_Date", date.ToString());
    }

    public void PlaySound(int i)
    {
        audioSound.clip = sounds[i];
        audioSound.Play();
    }


}

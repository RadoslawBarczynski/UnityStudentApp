using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static Supabase.Client;
using static UnityEditor.Progress;

public class GameManager : MonoBehaviour
{
    //db
    string url;
    string key;
    public Supabase.Client client;

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
            if (homework.isActive && !userDataLogged.homeworksToDelete.Contains(homework))
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
                userDataLogged.homeworksToDelete.Add(element);
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
      

    }


}

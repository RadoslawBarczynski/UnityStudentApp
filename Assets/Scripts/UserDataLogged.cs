using Assets.Scripts.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserDataLogged : MonoBehaviour
{
    public Guid UserID;
    public string Username;
    public Guid GradeID;
    public int Score;

    public bool isLoggedIn;

    //DB models
    public List<Student> students;
    public List<Grade> grades;
    public List<Question> questions;
    public List<Test> tests;
    public List<TestQuestion> testquestions;
    public List<Homework> homeworks;
}

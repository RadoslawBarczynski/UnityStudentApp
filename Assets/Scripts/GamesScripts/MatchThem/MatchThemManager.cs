using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class MatchThemManager : MonoBehaviour
{
    public float totalTime = 60f; 
    private float currentTime;   
    private bool timerRunning = false;
    public TextMeshProUGUI timerText;       
    public GameObject GameOverPanel;
    public GameManager gameManager;
    public TextMeshProUGUI EndPanelText;
    public UserDataLogged userDataLogged;
    public TextMeshProUGUI scoreText;

    private int score = 0;

    private void Start()
    {
        userDataLogged = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UserDataLogged>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        currentTime = totalTime;
        UpdateTimerText();
    }

    private void Update()
    {
        if (timerRunning)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerText();

            if (currentTime <= 0f)
            {
                timerRunning = false;
                currentTime = 0f;
                UpdateTimerText();
                GameOver();
            }
        }
    }

    public void StartTimer()
    {
        timerRunning = true;
    }

    private void UpdateTimerText()
    {
        timerText.text = currentTime.ToString("F2"); // Wyœwietlanie czasu z dwoma miejscami po przecinku
    }

    public void GameOver()
    {
        int points = score / 100;
        EndPanelText.text = "Zdoby³eœ " + points + " punktów do swojego ogólnego wyniku punktowego.";
        gameManager.CheckTasks(4, points);
        GameOverPanel.SetActive(true);
        gameManager.PlaySound(1);
        gameManager.UpdateScore(points, userDataLogged.UserID);
    }

    public void InscreseScore(int points)
    {
        SetScore(score + points);
    }

    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = "Punkty: " + score.ToString();
    }
}

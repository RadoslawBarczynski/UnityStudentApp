using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager2048 : MonoBehaviour
{
    public TileBoard board;
    public GameObject GameOverPanel;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestText;
    public TextMeshProUGUI EndPanelText;

    public UserDataLogged userDataLogged;
    public GameManager gameManager;

    private int score;

    private void Start()
    {
        userDataLogged = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UserDataLogged>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        NewGame();
    }

    public void NewGame()
    {
        SetScore(0);
        bestText.text = "Najlepszy wynik: " + LoadHiScore().ToString();

        GameOverPanel.SetActive(false);

        board.ClearBorad();
        board.CreateTile();
        board.CreateTile();
        board.enabled = true;
    }

    public void GameOver()
    {
        board.enabled = false;
        int points = score / 100;
        EndPanelText.text = "Zdoby³eœ " + points + " punktów do swojego ogólnego wyniku punktowego.";
        GameOverPanel.SetActive(true);
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

        SaveHiscore();
    }

    private void SaveHiscore()
    {
        int hiscore = LoadHiScore();

        if(score > hiscore)
        {
            PlayerPrefs.SetInt("hiscore", score);
        }
    }

    private int LoadHiScore()
    {
        return PlayerPrefs.GetInt("hiscore", 0);
    }
}

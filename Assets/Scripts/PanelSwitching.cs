using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelSwitching : MonoBehaviour
{
    //ui elements
    public GameObject loginPanel;
    public GameObject mainMenuPanel;
    public GameObject homeworkMenuPanel;
    public GameObject TestPanel;
    public GameObject GamesPanel;
    public GameObject FeedbackPanel;

    public GameManager gameManager;
    public UserDataLogged userDataLogged;

    public TextMeshProUGUI scoreText;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        userDataLogged = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UserDataLogged>();
    }

    private void Update()
    {
        scoreText.text = "Masz " + userDataLogged.Score + " punktow!";
    }

    public void ChangePanelFunction(int value)
    {
        if (value == 0)   //login panel
        {
            bool isActive = loginPanel.activeSelf;
            loginPanel.SetActive(!isActive);
        }
        else if (value == 1) //main menu
        {
            bool isActive = mainMenuPanel.activeSelf;
            mainMenuPanel.SetActive(!isActive);
        }
        else if (value == 2) //homework panel
        {
            bool isActive = homeworkMenuPanel.activeSelf;
            if (homeworkMenuPanel.activeSelf == true)
            {
                homeworkMenuPanel.GetComponent<LeanTweenPositionAnim>().OnDisable();
            }
            else if(homeworkMenuPanel.activeSelf == false)
            {
                homeworkMenuPanel.SetActive(!isActive);
                gameManager.SpawnHomework();
            }
            //homeworkMenuPanel.SetActive(!isActive);
        }
        else if(value == 3) //games panel
        {
            bool isActive = GamesPanel.activeSelf;
            if (isActive)
            {
                GamesPanel.GetComponent<LeanTweenPositionAnim>().OnDisable();
            }
            else
            {
                GamesPanel.SetActive(!isActive);
            }
        }
        else if (value == 4) //feedback panel
        {
            bool isActive = FeedbackPanel.activeSelf;
            if (isActive)
            {
                FeedbackPanel.GetComponent<LeanTweenPositionAnim>().OnDisable();
            }
            else
            {
                FeedbackPanel.SetActive(!isActive);
            }
        }

    }

    public void ResetApp()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}

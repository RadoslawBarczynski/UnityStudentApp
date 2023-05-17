using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LogsManager : MonoBehaviour
{
    //components
    public GameManager gameManager;
    public UserDataLogged userDataLogged;

    //ui elements
    public TextMeshProUGUI input;
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        userDataLogged = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UserDataLogged>();
    }

    public void InsertLog()
    {
        Logs log = new Logs {
        comment = input.text,
        datetime = DateTime.Now,
        user = userDataLogged.Username,
    };

        gameManager.InsertScore(log);
    }
}

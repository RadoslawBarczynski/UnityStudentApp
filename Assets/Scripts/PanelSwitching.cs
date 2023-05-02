using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelSwitching : MonoBehaviour
{
    //ui elements
    public GameObject loginPanel;
    public GameObject mainMenuPanel;
    public GameObject homeworkMenuPanel;
    public GameObject TestPanel;
    public GameObject GamesPanel;

    public void ChangePanelFunction(int value)
    {
        if (value == 0)
        {
            bool isActive = loginPanel.activeSelf;
            loginPanel.SetActive(!isActive);
        }
        else if (value == 1)
        {
            bool isActive = mainMenuPanel.activeSelf;
            mainMenuPanel.SetActive(!isActive);
        }
        else if (value == 2)
        {
            bool isActive = homeworkMenuPanel.activeSelf;
            if (isActive)
            {
                homeworkMenuPanel.GetComponent<LeanTweenPositionAnim>().OnDisable();
            }
            else
            {
                homeworkMenuPanel.SetActive(!isActive);
            }
            //homeworkMenuPanel.SetActive(!isActive);
        }
        else if(value == 3)
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
    }
}

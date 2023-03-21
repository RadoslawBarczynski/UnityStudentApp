using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //ui elements
    public GameObject loginPanel;
    public GameObject mainMenuPanel;
    public GameObject homeworkMenuPanel;

    public void ChangePanelFunction(int value)
    {
        if(value == 0)
        {
            bool isActive = loginPanel.activeSelf;
            loginPanel.SetActive(!isActive);
        }
        else if(value == 1)
        {
            bool isActive = mainMenuPanel.activeSelf;
            mainMenuPanel.SetActive(!isActive);
        }
        else if(value == 2)
        {
            bool isActive = homeworkMenuPanel.activeSelf;
            homeworkMenuPanel.SetActive(!isActive);
        }
    }

}

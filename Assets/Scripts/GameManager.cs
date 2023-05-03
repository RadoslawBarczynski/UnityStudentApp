using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //gameobjects
    public GameObject HomeworkParent;
    
    //prefabs
    public GameObject HomeworkElement;

    //components
    public UserDataLogged userDataLogged;

    public void SpawnHomework()
    {
        foreach(var homework in userDataLogged.homeworks)
        {
            if (homework.isActive)
            {
                GameObject clone = Instantiate(HomeworkElement, HomeworkParent.transform);
                clone.GetComponent<HomeworkSet>().numberOfGame = homework.GameNumber;
                clone.GetComponent<HomeworkSet>().SetTextFields(homework.ScoreToGet);
            }
        }
    }

}

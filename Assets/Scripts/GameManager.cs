using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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

        foreach(var item in userDataLogged.homeworksToDelete)
        {
            Debug.Log(item.id);
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


}

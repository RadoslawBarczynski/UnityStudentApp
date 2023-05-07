using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class HomeworkController : MonoBehaviour
{
    //gameobjects
    public List<GameObject> childrens;

    //prefabs
    public GameObject HomeworkElement;

    //components
    public UserDataLogged userDataLogged;



    void Start()
    {
        userDataLogged = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UserDataLogged>();
    }

    

}

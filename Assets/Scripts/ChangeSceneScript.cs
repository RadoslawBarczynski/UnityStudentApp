using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneScript : MonoBehaviour
{
    //components
    [SerializeField] GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    public void ChangeSceneToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void ChangeSceneToNumberCrush()
    {
        SceneManager.LoadScene("NumberCrush");
    }

    public void ChangeSceneToTests()
    {
        SceneManager.LoadScene("TestScene");
    }
    public void ChangeSceneToOperationEqualiser()
    {
        SceneManager.LoadScene("OperationEqualiser");
    }

    public void ChangeSceneTo2048()
    {
        SceneManager.LoadScene("2048");
    }

}


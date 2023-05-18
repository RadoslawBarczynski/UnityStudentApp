using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneScript : MonoBehaviour
{
    //components
    [SerializeField] GameManager gameManager;
    [SerializeField] Animator transition;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    public void ChangeSceneToMainMenu()
    {
        StartCoroutine(LoadLevel(0));
        //SceneManager.LoadScene("MainMenuScene");
    }

    public void ChangeSceneToNumberCrush()
    {
        StartCoroutine(LoadLevel(1));
        //SceneManager.LoadScene("NumberCrush");
    }

    public void ChangeSceneToTests()
    {
        StartCoroutine(LoadLevel(2));
        //SceneManager.LoadScene("TestScene");
    }
    public void ChangeSceneToOperationEqualiser()
    {
        StartCoroutine(LoadLevel(3));
        //SceneManager.LoadScene("OperationEqualiser");
    }

    public void ChangeSceneTo2048()
    {
        StartCoroutine(LoadLevel(4));
        //SceneManager.LoadScene("2048");
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(levelIndex);
    }

}


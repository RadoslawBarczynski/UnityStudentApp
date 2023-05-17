using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeworkSet : MonoBehaviour
{
    public TextMeshProUGUI description;
    public int numberOfGame;
    public string GameName;
    public Guid homeworkId;

    public void SetTextFields(int score)
    {
        if (numberOfGame == 1)
        {
            GameName = "rozbicie cyfr";
            description.text = "Uzyskaj " + score.ToString() + " punkt�w w grze " + GameName;
        }
        else if (numberOfGame == 2)
        {
            GameName = "wyr�wnaj r�wnanie";
            description.text = "Odpowiedz poprawnie na " + score.ToString() + " z 5 pyta� w grze " + GameName;
            return;
        }
        else if (numberOfGame == 3)
        {
            GameName = "2048";
            description.text = "Uzyskaj " + score.ToString() + " punkt�w w grze " + GameName;
        }
        else if (numberOfGame == 4)
        {
            GameName = "znajd� �cie�k�";
        }
        else
        {
            description.text = "Uzyskaj " + score.ToString() + " punkt�w w grze " + GameName;
        }
    }

    public void GameChangeScene()
    {
        if(numberOfGame == 1)
        {
            SceneManager.LoadScene("NumberCrush");
        }
        else if(numberOfGame == 2)
        {
            SceneManager.LoadScene("OperationEqualiser");
        }
        else if(numberOfGame == 3)
        {
            SceneManager.LoadScene("2048");
        }
        else if(numberOfGame == 4)
        {

        }
    }

}

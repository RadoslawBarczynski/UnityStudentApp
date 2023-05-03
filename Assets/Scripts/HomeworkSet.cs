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

    public void SetTextFields(int score)
    {
        if(numberOfGame == 1)
        {
            GameName = "rozbicie cyfr";
        }
        else if(numberOfGame == 2)
        {
            GameName = "wyrównaj równanie";
            description.text = "Uzyskaj " + score.ToString() + "% w grze " + GameName;
            return;
        }
        else if (numberOfGame == 3)
        {
            GameName = "2048";
        }
        else if (numberOfGame == 4)
        {
            GameName = "znajdŸ œcie¿kê";
        }

        description.text = "Uzyskaj " + score.ToString() + "punktów w grze " + GameName;
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

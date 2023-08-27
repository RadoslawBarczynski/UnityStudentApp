using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MatchThemManager : MonoBehaviour
{
    private System.Random random = new System.Random();

    private string[] operations = { "+", "-", "×", "/" };

    public List<GameObject> topElements = new List<GameObject>();
    public List<GameObject> bottomElements = new List<GameObject>();


    private List<string> previousOperations = new List<string>();

    public Dictionary<GameObject, Tuple<string, int>> correctElements = new Dictionary<GameObject, Tuple<string, int>>();

    private void Start()
    {
        GenerateMathObjects();
    }

    public string GenerateOperation()
    {
        return operations[random.Next(operations.Length)];
    }

    public int GenerateNumber(int min, int max)
    {
        return random.Next(min, max + 1);
    }

    public int CalculateResult(int num1, string operation, int num2)
    {
        switch (operation)
        {
            case "+":
                return num1 + num2;
            case "-":
                return num1 - num2;
            case "×":
                return num1 * num2;
            case "/":
                if (num2 != 0)
                    return num1 / num2;
                else
                    return int.MinValue; // Handle division by zero
            default:
                return int.MinValue; // Invalid operation
        }
    }

    public void GenerateMathObjects()
    {
        int correctIndex = random.Next(3); // Index of the correct math object

        string correctOperation = GenerateOperation();
        while (previousOperations.Contains(correctOperation))
        {
            correctOperation = GenerateOperation();
        }
        previousOperations.Add(correctOperation);

        int correctNumber1 = GenerateNumber(1, 10);
        int correctNumber2 = GenerateNumber(1, 10);

        int correctResult = CalculateResult(correctNumber1, correctOperation, correctNumber2);

        for (int i = 0; i < 3; i++)
        {
            string operation = GenerateOperation();
            while (previousOperations.Contains(operation))
            {
                operation = GenerateOperation();
            }
            previousOperations.Add(operation);

            int number1 = GenerateNumber(1, 10);
            int number2 = GenerateNumber(1, 10);

            if (i == correctIndex)
            {
                topElements[i].GetComponentInChildren<TextMeshProUGUI>().text = correctNumber1.ToString() + " " + correctOperation + " " + correctNumber2.ToString();
                bottomElements[i].GetComponentInChildren<TextMeshProUGUI>().text = correctResult.ToString();
                correctElements[topElements[i]] = new Tuple<string, int>(correctNumber1.ToString() + " " + correctOperation + " " + correctNumber2.ToString(), correctResult);
            }
            else
            {
                int result = CalculateResult(number1, operation, number2);

                topElements[i].GetComponentInChildren<TextMeshProUGUI>().text = number1.ToString() + " " + operation + " " + number2.ToString();

                // Randomly choose whether to display a result or an operation
                if (random.Next(2) == 0)
                {
                    bottomElements[i].GetComponentInChildren<TextMeshProUGUI>().text = result.ToString();
                    correctElements[topElements[i]] = new Tuple<string, int>(number1.ToString() + " " + operation + " " + number2.ToString(), result);
                }
                else
                {
                    bottomElements[i].GetComponentInChildren<TextMeshProUGUI>().text = CalculateResult(number1, operation, number2).ToString();
                    correctElements[topElements[i]] = new Tuple<string, int>(number1.ToString() + " " + operation + " " + number2.ToString(), result);
                }
            }
        }
    }

    public void CheckAnswer(string operation, int result)
    {
        bool flag = false;

        foreach (KeyValuePair<GameObject, Tuple<string, int>> kvp in correctElements)
        {
            if (kvp.Value.Item1 == operation && kvp.Value.Item2 == result)
            {
                Debug.Log("Correct answer! Operation: " + operation + ", Result: " + result);
                flag = true;
                correctElements.Remove(kvp.Key);
                break;
            }
        }

        if (flag)
        {
            //GenerateMathObjects();
        }
        else
        {
            Debug.Log("Incorrect answer! Keep trying.");
        }

    }
}

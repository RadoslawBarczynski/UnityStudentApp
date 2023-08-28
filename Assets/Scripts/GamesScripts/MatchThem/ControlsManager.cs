using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControlsManager : MonoBehaviour
{
    public List<GameObject> topElements = new List<GameObject>();
    public List<GameObject> bottomElements = new List<GameObject>();

    public GameObject resultPrefab;
    public GameObject operationPrefab;

    public Transform topParent;
    public Transform bottomParent;

    public PressLogic pressLogic;
    public MatchThemManager matchThemManager;

    public Dictionary<GameObject, Tuple<string, int>> correctElements = new Dictionary<GameObject, Tuple<string, int>>();

    private void Start()
    {
        for(int i = 0; i < 3; i++)
        {
            GenerateMathOperation();
        }

        ShuffleElements(topParent);
    }

    public void GenerateMathOperation()
    {
        // Generowanie losowych liczb naturalnych i operatora
        int operand1 = UnityEngine.Random.Range(1, 11);
        int operand2 = UnityEngine.Random.Range(1, 11);
        string operation;
        int result;
        char[] operators = { '+', '-', '×', '/' };
        char selectedOperator = operators[UnityEngine.Random.Range(0, operators.Length)];

        GameObject operationObject = Instantiate(operationPrefab, Vector2.zero, Quaternion.identity, topParent);
        GameObject resultObject = Instantiate(resultPrefab, Vector2.zero, Quaternion.identity, bottomParent);

        if (selectedOperator == '/')
        {
            while (operand1 % operand2 != 0)
            {
                operand1 = UnityEngine.Random.Range(1, 11);
                operand2 = UnityEngine.Random.Range(1, 11);
            }
        }

        // Tworzenie dzia³ania matematycznego
        if (operand1 >= operand2)
        {
            operation = $"{operand1} {selectedOperator} {operand2}";
            result = CalculateResult(operand1, operand2, selectedOperator);
        }
        else
        {
            operation = $"{operand2} {selectedOperator} {operand1}";
            result = CalculateResult(operand2, operand1, selectedOperator);
        }

        operationObject.GetComponentInChildren<TextMeshProUGUI>().text = operation;

        pressLogic.clickableElements.Add(operationObject.transform.GetChild(0).gameObject);

        resultObject.GetComponentInChildren<TextMeshProUGUI>().text = result.ToString();

        pressLogic.clickableElements.Add(resultObject.transform.GetChild(0).gameObject);

        correctElements.Add(operationObject, new Tuple<string, int>(operation, result));
    }

    private int CalculateResult(int operand1, int operand2, char selectedOperator)
    {
        int result = 0;

        switch (selectedOperator)
        {
            case '+':
                result = operand1 + operand2;
                break;
            case '-':
                result = operand1 - operand2;
                break;
            case '×':
                result = operand1 * operand2;
                break;
            case '/':
                if (operand2 != 0 && operand1 != 0 && operand1 % operand2 == 0)
                {
                    result = operand1 / operand2;
                }
                else
                {
                    Console.WriteLine("Niepoprawne dane dla dzielenia.");
                }
                break;
            default:
                Console.WriteLine("Nieznany operator.");
                break;
        }

        return result;
    }

    public bool CheckAnswer(string operation, int result)
    {
        bool flag = false;

        foreach (KeyValuePair<GameObject, Tuple<string, int>> kvp in correctElements)
        {
            if (kvp.Value.Item1 == operation && kvp.Value.Item2 == result)
            {
                Debug.Log("Correct answer! Operation: " + operation + ", Result: " + result);
                flag = true;
                correctElements.Remove(kvp.Key);
                matchThemManager.InscreseScore(5);
                break;
            }
        }

        if (flag)
        {
            return true;
        }
        else
        {
            Debug.Log("Incorrect answer! Keep trying.");
            return false;
        }

    }

    public void ShuffleElements(Transform parent)
    {
        int childCount = parent.childCount;
        List<Transform> children = new List<Transform>();

        // Dodawanie dzieci do listy
        for (int i = 0; i < childCount; i++)
        {
            children.Add(parent.GetChild(i));
        }

        // Tasowanie dzieci w losowej kolejnoœci
        System.Random rng = new System.Random();
        int n = children.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            Transform value = children[k];
            children[k] = children[n];
            children[n] = value;
        }

        // Przypisanie dzieci w nowej kolejnoœci
        for (int i = 0; i < childCount; i++)
        {
            children[i].SetSiblingIndex(i);
        }
    }
}

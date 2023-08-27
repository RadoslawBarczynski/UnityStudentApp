using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsManager : MonoBehaviour
{
    public GameObject LineObject;

    public void StartDraw()
    {
        LineObject.GetComponent<PressLogic>().enabled = true;
    }
}

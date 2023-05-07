using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeanTweenPositionAnim : MonoBehaviour
{

    private void OnEnable()
    {
        this.gameObject.transform.localPosition = new Vector2(0, -Screen.height);
        this.gameObject.transform.LeanMoveLocalY(0, 0.5f).setEaseOutExpo().delay = 0.1f;
    }

    public void OnDisable()
    {
        this.gameObject.transform.LeanMoveLocalY(-Screen.height, 0.2f).setEaseOutExpo().setOnComplete(OnComplete);
    }

    void OnComplete()
    {
        this.gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class PressLogic : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Vector3 startPos;
    private bool isDragging = false;
    private GameObject startElement;
    private GameObject endElement;
    public MatchThemManager matchThemManager;

    public List<GameObject> clickableElements = new List<GameObject>();

    public GraphicRaycaster raycaster;
    public EventSystem eventSystem;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            List<RaycastResult> results = new List<RaycastResult>();
            PointerEventData eventData = new PointerEventData(eventSystem);

            if (Input.GetMouseButtonDown(0))
            {
                eventData.position = Input.mousePosition;
            }
            else if (Input.touchCount > 0)
            {
                eventData.position = Input.GetTouch(0).position;
            }

            raycaster.Raycast(eventData, results);

            if (results.Count > 0 && clickableElements.Contains(results[0].gameObject))
            {
                lineRenderer.enabled = true;
                startPos = results[0].gameObject.transform.position;
                startPos.z = 0;
                lineRenderer.SetPosition(0, startPos);
                lineRenderer.SetPosition(1, startPos);
                startElement = results[0].gameObject;
                isDragging = true;
            }
        }
        else if ((Input.GetMouseButton(0) && isDragging) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved && isDragging))
        {
            lineRenderer.enabled = true;
            Vector3 currentPos;

            if (Input.GetMouseButton(0))
            {
                currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            else
            {
                currentPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            }

            currentPos.z = 0;
            lineRenderer.SetPosition(1, currentPos);
        }
        else if ((Input.GetMouseButtonUp(0) && isDragging) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && isDragging))
        {
            List<RaycastResult> results = new List<RaycastResult>();
            PointerEventData eventData = new PointerEventData(eventSystem);

            if (Input.GetMouseButtonUp(0))
            {
                eventData.position = Input.mousePosition;
            }
            else
            {
                eventData.position = Input.GetTouch(0).position;
            }

            raycaster.Raycast(eventData, results);

            lineRenderer.enabled = false;
            isDragging = false;

            if (results.Count > 0 && clickableElements.Contains(results[0].gameObject))
            {
                endElement = results[0].gameObject;
                matchThemManager.CheckAnswer(startElement.GetComponent<TextMeshProUGUI>().text, int.Parse(endElement.GetComponent<TextMeshProUGUI>().text));
                //Debug.Log("Started dragging from: " + startElement.GetComponent<TextMeshProUGUI>().text + ", Ended dragging on: " + int.Parse(endElement.GetComponent<TextMeshProUGUI>().text));
                //if(matchThemManager.CheckAnswer(startElement.GetComponent<TextMeshProUGUI>().text, int.Parse(endElement.GetComponent<TextMeshProUGUI>().text)))
                //{
                //  matchThemManager.GenerateMathObjects();
                //}
            }
        }
    }
}

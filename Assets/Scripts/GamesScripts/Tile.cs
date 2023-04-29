using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tile : MonoBehaviour, IPointerDownHandler
{
    private static Tile selected; // 1 
    private Image image; // 2
    public Vector2Int Position;

    private void Start() // 3
    {
        image = GetComponent<Image>();
    }

    public void Select() // 4
    {
        image.color = Color.grey;
    }

    public void Unselect() // 5 
    {
        image.color = Color.white;
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if (selected != null)
        {
            if (selected == this)
                return;
            selected.Unselect();
            if (Vector2Int.Distance(selected.Position, Position) == 1)
            {
                GridManagement.Instance.SwapTiles(Position, selected.Position);
                selected = null;
            }
            else
            {
                selected = this;
                Select();
            }
        }
        else
        {
            selected = this;
            Select();
        }
    }
}

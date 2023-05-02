using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tile2048 : MonoBehaviour
{
    public TileState state { get; private set; }
    public TileCell cell { get; private set; }
    public int number { get; private set; }
    public bool locked { get; set; }

    private Image background;
    private TextMeshProUGUI text;
    public TileBoard tileboard;

    private void Awake()
    {
        background = GetComponent<Image>();
        text = GetComponentInChildren<TextMeshProUGUI>();
        tileboard = GameObject.Find("Board").GetComponent<TileBoard>();
    }

    public void SetState(TileState state, int number)
    {
        this.state = state;
        this.number = number;

        background.color = state.BackgroundColor;
        text.color = state.TextColor;
        text.text = number.ToString();
    }

    public void Spawn(TileCell cell)
    {
        if(this.cell != null)
        {
            this.cell.tile = null;
        }

        this.cell = cell;
        this.cell.tile = this;

        transform.position = cell.transform.position;
    }

    public void MoveTo(TileCell cell)
    {
        if (this.cell != null)
        {
            this.cell.tile = null;
        }

        this.cell = cell;
        this.cell.tile = this;

        //transform.position = cell.transform.position;
        LeanTween.move(gameObject, cell.transform.position, 0.15f).setOnComplete(OnComplete);
    }

    private void OnComplete()
    {
        TileBoard.waiting = false;
        //tileboard.AfterChangesFunc();
    }

    public void Merge(TileCell cell)
    {
        if (this.cell != null)
        {
            this.cell.tile = null;
        }

        this.cell = null;
        cell.tile.locked = true;

        LeanTween.move(gameObject, cell.transform.position, 0.1f).setOnComplete(OnComplete);
        Destroy(gameObject);
    }
}

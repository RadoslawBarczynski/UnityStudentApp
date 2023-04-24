using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridManagement : MonoBehaviour
{
    public List<Sprite> Sprites = new List<Sprite>();
    public GameObject TilePrefab;
    public int GridDimension = 5;
    public float Distance = 1.0f;
    private GameObject[,] Grid;
    // Start is called before the first frame update
    void Start()
    {
        Grid = new GameObject[GridDimension, GridDimension];
        InitGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitGrid()
    {
        Vector3 positionOffset = transform.position - new Vector3(GridDimension * Distance / 2.0f, GridDimension * Distance / 2.0f, 0); // 1
        for (int row = 0; row < GridDimension; row++)
            for (int column = 0; column < GridDimension; column++) // 2
            {
                List<Sprite> possibleSprites = new List<Sprite>(Sprites); // 1

                //Choose what sprite to use for this cell
                Sprite left1 = GetSpriteAt(column - 1, row); //2
                Sprite left2 = GetSpriteAt(column - 2, row);
                if (left2 != null && left1 == left2) // 3
                {
                    possibleSprites.Remove(left1); // 4
                }

                Sprite down1 = GetSpriteAt(column, row - 1); // 5
                Sprite down2 = GetSpriteAt(column, row - 2);
                if (down2 != null && down1 == down2)
                {
                    possibleSprites.Remove(down1);
                }

                GameObject newTile = Instantiate(TilePrefab); // 3
                Image image = newTile.GetComponent<Image>(); // 4
                image.sprite = possibleSprites[Random.Range(0, possibleSprites.Count)]; // 5
                newTile.transform.parent = transform; // 6
                newTile.transform.position = new Vector3(column * Distance, row * Distance, 0) + positionOffset; // 7

                Grid[column, row] = newTile; // 8
            }
    }

    Sprite GetSpriteAt(int column, int row)
    {
        if (column < 0 || column >= GridDimension
            || row < 0 || row >= GridDimension)
            return null;
        GameObject tile = Grid[column, row];
        Image renderer = tile.GetComponent<Image>();
        return renderer.sprite;
    }
}

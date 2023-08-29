using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GridManagement : MonoBehaviour
{
    public List<Sprite> Sprites = new List<Sprite>();
    public GameObject TilePrefab;
    public GameObject GameOverMenu;
    public GameObject floatingTextPrefab;

    public int GridDimension = 5;
    public float Distance = 210f;
    public int Score = 0;
    public int StartingMoves = 30;
    private int _numMoves;
    private GameObject[,] Grid;
    public TextMeshProUGUI MovesText;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI EndPanelText;

    //components
    public GameManager gameManager;
    public UserDataLogged userDataLogged;
    public static GridManagement Instance { get; private set; }

    public int NumMoves
    {
        get
        {
            return _numMoves;
        }

        set
        {
            _numMoves = value;
            MovesText.text = _numMoves.ToString();
        }
    }

    private void Awake()
    {
        Instance = this;
        NumMoves = StartingMoves;
    }

    void Start()
    {
        Grid = new GameObject[GridDimension, GridDimension];
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        userDataLogged = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UserDataLogged>();
        InitGrid();
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = "Score: " + Score.ToString();
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

                Tile tile = newTile.AddComponent<Tile>();
                tile.Position = new Vector2Int(column, row);

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

    public void SwapTiles(Vector2Int tile1Position, Vector2Int tile2Position) // 1
    {

        // 2
        GameObject tile1 = Grid[tile1Position.x, tile1Position.y];
        Image image1 = tile1.GetComponent<Image>();

        GameObject tile2 = Grid[tile2Position.x, tile2Position.y];
        Image image2 = tile2.GetComponent<Image>();

        // 3
        Sprite temp = image1.sprite;
        image1.sprite = image2.sprite;
        image2.sprite = temp;

        bool changesOccurs = CheckMatches();
        if (!changesOccurs)
        {
            temp = image1.sprite;
            image1.sprite = image2.sprite;
            image2.sprite = temp;
        }
        else
        {
            NumMoves--;
            do
            {
                FillHoles();
            } while (CheckMatches());
            if(NumMoves <= 0)
            {
                NumMoves = 0; GameOver();
            }
        }
    }

    Image GetSpriteRendererAt(int column, int row)
    {
        if (column < 0 || column >= GridDimension
             || row < 0 || row >= GridDimension)
            return null;
        GameObject tile = Grid[column, row];
        Image image = tile.GetComponent<Image>();
        return image;
    }

    bool CheckMatches()
    {
        HashSet<Image> matchedTiles = new HashSet<Image>(); // 1
        for (int row = 0; row < GridDimension; row++)
        {
            for (int column = 0; column < GridDimension; column++) // 2
            {
                Image current = GetSpriteRendererAt(column, row); // 3

                List<Image> horizontalMatches = FindColumnMatchForTile(column, row, current.sprite); // 4
                if (horizontalMatches.Count >= 2)
                {
                    matchedTiles.UnionWith(horizontalMatches);
                    matchedTiles.Add(current); // 5
                }

                List<Image> verticalMatches = FindRowMatchForTile(column, row, current.sprite); // 6
                if (verticalMatches.Count >= 2)
                {
                    matchedTiles.UnionWith(verticalMatches);
                    matchedTiles.Add(current);
                }
            }
        }

        foreach (Image images in matchedTiles) // 7
        {
            LeanTween.scale(images.gameObject, new Vector3(0, 0, 0), 0.2f);
            images.sprite = null;
        }
        Score += matchedTiles.Count;
        GameObject floatingPoints = Instantiate(floatingTextPrefab, ScoreText.gameObject.transform.position, Quaternion.identity, ScoreText.gameObject.transform.parent);
        floatingPoints.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = matchedTiles.Count.ToString();
        return matchedTiles.Count > 0; // 8
    }

    List<Image> FindColumnMatchForTile(int col, int row, Sprite sprite)
    {
        List<Image> result = new List<Image>();
        for (int i = col + 1; i < GridDimension; i++)
        {
            Image nextColumn = GetSpriteRendererAt(i, row);
            if (nextColumn.sprite != sprite)
            {
                break;
            }
            result.Add(nextColumn);
        }
        return result;
    }

    List<Image> FindRowMatchForTile(int col, int row, Sprite sprite)
    {
        List<Image> result = new List<Image>();
        for (int i = row + 1; i < GridDimension; i++)
        {
            Image nextRow = GetSpriteRendererAt(col, i);
            if (nextRow.sprite != sprite)
            {
                break;
            }
            result.Add(nextRow);
        }
        return result;
    }

    void FillHoles()
    {
        for (int column = 0; column < GridDimension; column++)
        {
            for (int row = 0; row < GridDimension; row++) // 1
            {
                while (GetSpriteRendererAt(column, row).sprite == null) // 2
                {
                    for (int filler = row; filler < GridDimension - 1; filler++) // 3
                    {
                        Image current = GetSpriteRendererAt(column, filler); // 4
                        Image next = GetSpriteRendererAt(column, filler + 1);
                        current.sprite = next.sprite;
                        LeanTween.scale(current.gameObject, new Vector3(0.795845f, 0.795845f, 0.795845f), 0.2f);
                    }
                    Image last = GetSpriteRendererAt(column, GridDimension - 1);
                    last.sprite = Sprites[Random.Range(0, Sprites.Count)];
                    LeanTween.scale(last.gameObject, new Vector3(0.795845f, 0.795845f, 0.795845f), 0.2f);
                }
            }
        }
    }

    void GameOver()
    {
        int points = Score / 100;
        EndPanelText.text = "Otrzymujesz " + points + " punktów do swojego ogólnego wyniku punktowego.";
        GameOverMenu.SetActive(true);
        gameManager.PlaySound(1);
        gameManager.CheckTasks(1, points);
        gameManager.UpdateScore(points, userDataLogged.UserID);
    }
}

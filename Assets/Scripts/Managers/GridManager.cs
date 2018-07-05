using UnityEngine;

public class GridManager : Singleton<GridManager>
{
    [SerializeField] private Transform playArea;
    [SerializeField] private int gridSize = 10;
    [SerializeField] private GridTile gridTilePrefab;

    public int GridSize { get { return gridSize; } }
    Vector3 startPoint;
    public Vector3 StartPoint { get { return startPoint; } }

    private int width;
    private int height;
    private Transform[,] grid;

    void Start()
    {
        InitializeGrid();
    }

    void Update()
    {
    }

    public void InitializeGrid()
    {
        width = Mathf.RoundToInt(playArea.localScale.x * gridSize);
        height = Mathf.RoundToInt(playArea.localScale.y * gridSize);

        grid = new Transform[width, height];
        startPoint = playArea.GetComponent<Renderer>().bounds.min;
        var x = startPoint.x + 0.1f;
        var y = startPoint.y + 0.1f;
        startPoint = new Vector2(x, y);

        CreateGridTiles();
    }

    private void CreateGridTiles()
    {
        if (gridTilePrefab == null) return;
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                var worldPos = GetWorldPos(x, y);
                var gridTile = Instantiate(gridTilePrefab, worldPos, Quaternion.identity);
                gridTile.transform.parent = transform;
                gridTile.name = string.Format("Tile({0},{1})", x, y);
                gridTile.MoveToGrid(x, y);
                grid[x, y] = gridTile.transform;
            }
        }
    }

    private Vector3 GetWorldPos(int x, int y)
    {
        float xf = x;
        float yf = y;
        return new Vector3(startPoint.x + (xf / gridSize), startPoint.y + (yf / gridSize), startPoint.z - 0.1f);
    }

    public GridTile GetRandomTile(int margin = 0)
    {
        if ((margin > width || margin > height) && margin < 0)
            return GetTileAt(0, 0);

        var x = Random.Range(0 + margin, width - margin); // margin =1; 1, 5.6
        var y = Random.Range(0 + margin, height - margin);// 1, 3.4
        return GetTileAt(x, y);
    }

    public GridTile GetTileAt(int x, int y)
    {
        if ((x < width && x >= 0) && (y < height && y >= 0))
        {
            if (grid[x, y] != null)
                return grid[x, y].GetComponent<GridTile>();
            return null;
        }
        else
        {
            GameState.SetGameState(State.GameOver);
            return null;
        }
    }

    public void Reset()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        
        InitializeGrid();
    }
}
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [SerializeField] private SnakeTile snakeHeadTilePrefab;
    [SerializeField] private SnakeTile snakeTilePrefab;
    private List<SnakeTile> snakePieces;

    public List<SnakeTile> SnakePieces { get { return snakePieces; } }

    void Start()
    {
//        InitializeSnake();
        
    }

    public void InitializeSnake()
    {
        snakePieces = new List<SnakeTile>();

        GameManager.OnApplePickup += AppleEaten;
        var gridTile = GridManager.Instance.GetRandomTile(5);
        transform.position = gridTile.transform.position;

        if (snakePieces.Count < 2)
        {
            var snakeTile = Instantiate(snakeHeadTilePrefab, transform.position, Quaternion.identity);
            snakeTile.transform.parent = transform;
            snakeTile.MoveToTile(gridTile);
            snakePieces.Add(snakeTile);
        }
        else
        {
            var snakeTile = Instantiate(snakeTilePrefab, transform.position, Quaternion.identity);
            snakeTile.transform.parent = transform;
            snakeTile.MoveToTile(gridTile);
            snakePieces.Add(snakeTile);
        }
    }

    private void AppleEaten()
    {
        AddPiece();
    }

    void AddPiece()
    {
        SnakeTile snakeTile = Instantiate(snakeTilePrefab, transform.position, Quaternion.identity);
        snakeTile.transform.parent = transform;
        snakeTile.MoveToTile(snakePieces[snakePieces.Count - 1]);

        switch (PlayerInput.Dir)
        {
            case PlayerInput.Direction.Up:
                snakeTile.MoveDown();
                break;
            case PlayerInput.Direction.Down:
                snakeTile.MoveUp();
                break;
            case PlayerInput.Direction.Left:
                snakeTile.MoveRight();
                break;
            case PlayerInput.Direction.Right:
                snakeTile.MoveLeft();
                break;
            default: break;
        }
        snakePieces.Add(snakeTile);
    }

    void Move()
    {
        for (var i = snakePieces.Count - 1; i >= 0; i--)
        {
            if (i == 0)
            {
                switch (PlayerInput.Dir)
                {
                    case PlayerInput.Direction.Up:
                        snakePieces[i].MoveUp();
                        break;
                    case PlayerInput.Direction.Down:
                        snakePieces[i].MoveDown();
                        break;
                    case PlayerInput.Direction.Left:
                        snakePieces[i].MoveLeft();
                        break;
                    case PlayerInput.Direction.Right:
                        snakePieces[i].MoveRight();
                        break;
                    default: break;
                }
            }
            else
            {
                snakePieces[i].MoveToTile(snakePieces[i - 1]);
            }
        }
    }

    public void Tick()
    {
        Move();
    }

    public SnakeTile GetHead()
    {
        return snakePieces[0];
    }

    public void Reset()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        GameManager.OnApplePickup -= AppleEaten;
        InitializeSnake();
    }
}
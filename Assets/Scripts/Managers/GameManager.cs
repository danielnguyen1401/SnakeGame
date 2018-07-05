using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject GameOverText;
    [SerializeField] private GameObject PauseText;
    [SerializeField] private float timeToMove = 0.5f;

    private int score;
    public int Score { get { return score; } }

    private Snake _snake;
    private bool snakeIsOutOfBounds = false;
    public static Action OnApplePickup;
    public static Action OnScoreAdd;

    private void Start()
    {
        GridManager.Instance.InitializeGrid();
        _snake = FindObjectOfType<Snake>();
        
        _snake.InitializeSnake();

        GameState.OnGameOver += GameOver;
        GameState.OnPlaying += Playing;
        GameState.OnPaused += Paused;

        PlayerInput.AnyKeyPress += AnyKeyPressed;
        PlayerInput.PausePress += PausePressed;

        GameState.SetGameState(State.Playing);

        SpawnApple();
    }

    private void PausePressed()
    {
        if (GameState.GetGameState() == State.Paused)
            GameState.SetGameState(State.Playing);
        else if (GameState.GetGameState() == State.Playing)
            GameState.SetGameState(State.Paused);
    }

    private void AnyKeyPressed()
    {
        if (GameState.GetGameState() == State.GameOver)
        {
            RestartGame();
        }
    }

    private void RestartGame()
    {
        score = 0;
        GridManager.Instance.Reset();
        _snake.Reset();
        SpawnApple();
        GameState.SetGameState(State.Playing);
    }

    private void Paused()
    {
        StartCoroutine(PauseRoutine());
    }

    IEnumerator PauseRoutine()
    {
        PauseText.SetActive(true);
        while (GameState.GetGameState() == State.Paused)
        {
            yield return new WaitForEndOfFrame();
        }
        PauseText.SetActive(false);
    }

    private void Playing()
    {
        StartCoroutine(PlayRoutine());
    }

    IEnumerator PlayRoutine()
    {
        while (GameState.GetGameState() == State.Playing)
        {
            _snake.Tick();
            if (snakeIsOutOfBounds)
                GameState.SetGameState(State.GameOver);
            else
            {
                CheckIfSnakeOnApple();
                CheckIfSnakeOnSnake();
            }
            AddScore(1);
            yield return new WaitForSeconds(timeToMove);
        }
    }

    void AddScore(int value)
    {
        score += value;
        if (OnScoreAdd != null) OnScoreAdd();
    }

    private void CheckIfSnakeOnSnake()
    {
        var snakeHeadGridPosition = _snake.GetHead().GridPos;
        foreach (var snakeTile in _snake.SnakePieces.Skip(1))
        {
            if (snakeTile.GridPos == snakeHeadGridPosition)
            {
                GameState.SetGameState(State.GameOver);
            }
        }
    }

    private void CheckIfSnakeOnApple()
    {
        SnakeTile tileAtHead = _snake.GetHead();
        var gridTile = GridManager.Instance.GetTileAt(tileAtHead.GridPos.x, tileAtHead.GridPos.y);

        if (gridTile == null) return;

        if (gridTile.HasApple)
        {
            SnakeOnApple(gridTile);
            SpawnApple();
        }
    }

    private void SpawnApple()
    {
        GridTile tile = GridManager.Instance.GetRandomTile();
        tile.SetApple();
    }

    private void SnakeOnApple(GridTile gridTile)
    {
        gridTile.TakeApple();
        OnApplePickup();
        AddScore(10);
    }

    private void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        GameOverText.SetActive(true);
        while (GameState.GetGameState() == State.GameOver)
        {
            yield return new WaitForEndOfFrame();
        }
        GameOverText.SetActive(false);
    }
}
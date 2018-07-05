using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public enum Direction
    {
        Up,
        Left,
        Right,
        Down
    }

    public static Direction Dir;
    public static Action PausePress;
    public static Action AnyKeyPress;

    private void Update()
    {
        switch (GameState.GetGameState())
        {
            case State.Playing:
                HandleInputPlaying();
                HandleInputPause();
                break;
            case State.Paused:
                HandleInputPause();
                break;
            case State.GameOver:
                HandleInputGameOver();
                break;
            default: break;
        }
    }

    void HandleInputPlaying()
    {
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");
        if (Mathf.Abs(v) > Mathf.Abs(h))
            Dir = v > 0 ? Direction.Up : Direction.Down;
        else if (Mathf.Abs(h) > Mathf.Abs(v))
            Dir = h > 0 ? Direction.Right : Direction.Left;
    }

    void HandleInputPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            PausePress();
    }

    void HandleInputGameOver()
    {
        if (Input.anyKeyDown)
            AnyKeyPress();
    }
}
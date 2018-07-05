using System;
using UnityEngine;


public enum State
{
    Playing,
    Paused,
    GameOver
}

public class GameState : MonoBehaviour
{
    private static State gamesState;
    public static Action OnGameOver;
    public static Action OnPaused;
    public static Action OnPlaying;

    public static State GetGameState()
    {
        return gamesState;
    }

    public static void SetGameState(State state)
    {
        switch (state)
        {
            case State.Playing:
                SetStatePlaying();
                break;
            case State.Paused:
                SetStatePaused();
                break;
            case State.GameOver:
                SetStateGameOver();
                break;
            default: break;
        }
    }

    static void SetStatePlaying()
    {
        gamesState = State.Playing;
        OnPlaying();
    }

    static void SetStatePaused()
    {
        gamesState = State.Paused;
        OnPaused();
    }

    static void SetStateGameOver()
    {
        gamesState = State.GameOver;
        OnGameOver();
    }
}
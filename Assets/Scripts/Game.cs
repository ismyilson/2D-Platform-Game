using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    GAME_RUNNING,
    GAME_PAUSED,
    GAME_GAMEOVER
}

public class Game : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Game started");

        CurrentState = GameState.GAME_RUNNING;

        UITimerCounter = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Game is paused
        if (CurrentState.Equals(GameState.GAME_PAUSED))
        {
            return;
        }

        // Game timer
        UITimerCounter += Time.deltaTime;
        if (UITimerCounter >= UITimerCounterEnd)
        {
            UITimerCounter = 0.0f;
            PlayerUI.UpdateTimer();
        }
    }

    public static bool IsPaused()
    {
        return CurrentState.Equals(GameState.GAME_PAUSED);
    }
    
    public static GameState GetGameState()
    {
        return CurrentState;
    }

    public static void PauseGame()
    {
        if (IsPaused())
        {
            return;
        }
        
        CurrentState = GameState.GAME_PAUSED;

        Player.SetInvencible(true);
        Player.SetCanMove(false);
    }

    public static void UnpauseGame()
    {
        if (!IsPaused())
        {
            return;
        }
        
        CurrentState = GameState.GAME_RUNNING;

        Player.SetInvencible(false);
        Player.SetCanMove(true);
    }

    public static void GameOver()
    {
        Debug.Log("Game over");

        CurrentState = GameState.GAME_GAMEOVER;
    }

    static GameState CurrentState;

    float UITimerCounter;
    const float UITimerCounterEnd = 1.0f;
}
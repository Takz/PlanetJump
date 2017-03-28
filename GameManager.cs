using UnityEngine;
using System.Collections;
using System;

public class GameState:MonoBehaviour
{
    public static GameState instance = null;

    public enum States
    {
        Start, GameOver, Pause, Running
    }
    public static States state = States.Start;
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
    }

    void Start()
    {
        state = States.Start;
    }

    void Update()
    {
        Debug.Log(state);
    }

    public static void ChangeState(States stateTo)
    {
        if (state == stateTo)
            return;
        state = stateTo;
    }

    public static bool IsState(States stateTo)
    {
        if (state == stateTo)
            return true;
        return false;
    }

    public static bool IsRunning
    {
        get
        {
            return IsState(States.Running);
        }
    }

    public static bool IsPaused
    {
        get
        {
            return IsState(States.Pause);
        }
    }

    public static bool IsGameOver
    {
        get
        {
            return IsState(States.GameOver);
        }
    }

}
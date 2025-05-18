using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameFSM
{
    Dictionary<Type, GameState> _states = new Dictionary<Type, GameState>();
    GameState _currentState;

    public GameState CurrentState => _currentState;

    public GameFSM()
    {
        AddStates();
    }

    void AddStates()
    {
        AddState(new GameState_Menu());
        AddState(new GameState_Gameplay());
        AddState(new GameState_Pause());
    }

    void AddState(GameState state)
    {
        _states.Add(state.GetType(), state);
    }



    public async UniTaskVoid SetState<T>() where T : GameState
    {
        var type = typeof(T);

        if (_currentState != null && _currentState.GetType() == type)
        {
            Debug.Log("Wrong state " + type.ToString());
            return;
        }

        if(_states.TryGetValue(type, out var newState))
        {
            if (_currentState != null)
                await _currentState.Exit();
            _currentState = newState;
            _currentState.Enter();
        }
        else
            Debug.LogError("No state " + type.ToString());
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

public class StateMachine : IStateSwitcher
{
    private List<IState> _states = new();
    private IState _currentState;
    private IState _previousState;

    public void AddState<T>(T state) where T : IState
    {
        if (_states.Any(st => st is T))
            throw new ArgumentException($"StateMachine is already exist {typeof(T)}");

        _states.Add(state);
    }

    public void SwitchState<T>() where T : IState
    {
        _currentState?.Exit();
        var previousState = _currentState;
        _currentState = _states.FirstOrDefault(state => state is T);
        _previousState = previousState ?? _currentState;
        _currentState.Enter();
    }

    public void Update() => _currentState.Update();

    public void FixedUpdate() => _currentState.FixedUpdate();

    public void SetPreviousState()
    {
        _currentState.Exit();
        _currentState = _previousState;
        _previousState = _currentState;
        _currentState.Enter();
    }
}

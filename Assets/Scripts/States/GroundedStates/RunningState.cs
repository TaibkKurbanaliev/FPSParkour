using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RunningState : GroundedState
{
    public RunningState(Player player, IStateSwitcher stateSwitcher, GroundStateConfig config) : base(player, stateSwitcher, config)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Player.Input.Player.SlideCrouch.started += OnSlideStarted;
        Player.StatesData.MaxHorizontalSpeed = Config.RunSpeed;
        Player.StatesData.Acceleration = Config.RunAcceleration;
    }

    public override void Exit()
    {
        base.Exit();
        Player.Input.Player.SlideCrouch.started -= OnSlideStarted;
    }

    public override void Update()
    {
        base.Update();

        if (!Player.Input.Player.Sprint.inProgress)
            StateSwitcher.SwitchState<WalkingState>();
    }

    private void OnSlideStarted(InputAction.CallbackContext context)
    {
        StateSwitcher.SwitchState<SlidingState>();
    }
}

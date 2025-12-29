using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WalkingState : GroundedState
{
    private float _velocityOffset = 0.0001f;

    public WalkingState(Player player, IStateSwitcher stateSwitcher, GroundStateConfig config) : base(player, stateSwitcher, config)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Player.StatesData.MaxHorizontalSpeed = Config.WalkSpeed;
        Player.StatesData.Acceleration = Config.WalkAcceleration;
        Player.Input.Player.SlideCrouch.started += OnCrouchStarted;
    }

    public override void Exit()
    {
        base.Exit();

        Player.Input.Player.SlideCrouch.started -= OnCrouchStarted;
    }

    public override void Update()
    {
        base.Update();

        if (Player.Rigidbody.velocity.magnitude <= _velocityOffset)
            StateSwitcher.SwitchState<IdleState>();
        else if (Player.Input.Player.Sprint.IsPressed())
            StateSwitcher.SwitchState<RunningState>();
    }
}

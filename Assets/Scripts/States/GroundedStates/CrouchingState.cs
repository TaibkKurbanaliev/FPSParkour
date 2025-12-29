using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CrouchingState : GroundedState
{
    private float _originalHeight;

    public CrouchingState(Player player, IStateSwitcher stateSwitcher, GroundStateConfig config) 
        : base(player, stateSwitcher, config)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _originalHeight = Player.Collider.height;

        Player.StatesData.Acceleration = Config.CrouchAcceleration;
        Player.StatesData.MaxHorizontalSpeed = Config.CrouchSpeed;
        Player.Collider.height = Player.Collider.radius * 2f + Config.CrouchHeight;
        Player.Collider.center = new Vector3(0f, 
                                             -((_originalHeight - Player.Collider.height) / 2f), 
                                             0f);
        Player.Input.Player.SlideCrouch.started += OnExitCrouch;
    }


    public override void Exit()
    {
        base.Exit();

        Player.Collider.height = _originalHeight;
        Player.Collider.center = Vector3.zero;
        Player.Input.Player.SlideCrouch.started -= OnExitCrouch;
    }

    private void OnExitCrouch(InputAction.CallbackContext context)
    {
        if (Player.Checker.CheckHead())
            return;

        if (Player.Input.Player.Move.ReadValue<Vector2>() == Vector2.zero)
            StateSwitcher.SwitchState<IdleState>();
        else
            StateSwitcher.SwitchState<WalkingState>();
    }
}

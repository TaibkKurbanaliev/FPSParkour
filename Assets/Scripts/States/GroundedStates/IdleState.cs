using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IdleState : GroundedState
{
    public IdleState(Player player, IStateSwitcher stateSwitcher, GroundStateConfig config) 
        : base(player, stateSwitcher, config)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Player.StatesData.MaxHorizontalSpeed = 0f;
        Player.Input.Player.SlideCrouch.started += OnCrouchStarted;
    }

    public override void Exit()
    {
        base.Exit();

        Player.Input.Player.SlideCrouch.started -= OnCrouchStarted;
    }

    public override void Update()
    {
        if (Player.Input.Player.Move.ReadValue<Vector2>() != Vector2.zero)
            StateSwitcher.SwitchState<WalkingState>();

        base.Update();
    }

    
}

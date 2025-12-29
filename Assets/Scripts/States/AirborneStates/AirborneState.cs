using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class AirborneState : MovementState
{
    protected AirborneStateConfig Config { get; private set; }
    public AirborneState(Player player, IStateSwitcher stateSwitcher, AirborneStateConfig config) 
                       : base(player, stateSwitcher)
    {
        Config = config;
    }

    public override void Enter()
    {
        base.Enter();

        Player.StatesData.Acceleration *= Config.AirFriction;
        Player.StatesData.Drag = 0;
        Player.Input.Player.Dash.started += OnDashStarted;
        Player.Input.Player.Jump.started += OnJumpStarted;
    }

    public override void Exit()
    {
        base.Exit();

        Player.Input.Player.Dash.started -= OnDashStarted;
        Player.Input.Player.Jump.started -= OnJumpStarted;
    }

    public override void FixedUpdate()
    {   
        base.FixedUpdate();

        if (Player.StatesData.IsGronded)
        {
            StateSwitcher.SwitchState<WalkingState>();
            return;
        }

        if (Player.StatesData.IsWalled)
        {
            StateSwitcher.SwitchState<WallRunningState>();
        }
    }

    private void OnDashStarted(InputAction.CallbackContext context)
    {
        if (Player.StatesData.IsDashReload == false)
            StateSwitcher.SwitchState<AirDashState>();
    }

    private void OnJumpStarted(InputAction.CallbackContext context)
    {
        if (Player.StatesData.NumberOfAvailableJumps > 0)
            StateSwitcher.SwitchState<JumpingState>();
    }
}

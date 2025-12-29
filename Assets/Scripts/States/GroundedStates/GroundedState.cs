using UnityEngine;
using UnityEngine.InputSystem;

public abstract class GroundedState : MovementState
{
    protected GroundStateConfig Config;

    protected GroundedState(Player player, IStateSwitcher stateSwitcher, GroundStateConfig config) 
        : base(player, stateSwitcher)
    {
        Config = config;
    }

    public override void Enter()
    {
        base.Enter();
        Player.Input.Player.Jump.started += OnJumpStarted;
        Player.Input.Player.Dash.started += OnDashStarted;
        Player.StatesData.Drag = Config.Drag;
        Player.StatesData.NumberOfAvailableJumps = Config.NumberOfJumps;
    }

    public override void Exit()
    {
        base.Exit();
        Player.Input.Player.Jump.started -= OnJumpStarted;
        Player.Input.Player.Dash.started -= OnDashStarted;
    }

    public override void Update()
    {
        base.Update();

        if (!Player.StatesData.IsGronded && Player.Rigidbody.velocity.y < 0)
            StateSwitcher.SwitchState<FallingState>();
        else if (Player.StatesData.SlopeAngle >= Config.MaxSlopeAngle)
            StateSwitcher.SwitchState<SlideDownState>();
    }

    protected void OnCrouchStarted(InputAction.CallbackContext context)
    {
        StateSwitcher.SwitchState<CrouchingState>();
    }

    private void OnJumpStarted(InputAction.CallbackContext context)
    {
        StateSwitcher.SwitchState<JumpingState>();
    }

    private void OnDashStarted(InputAction.CallbackContext context)
    {
        if (Player.StatesData.IsDashReload == false)
            StateSwitcher.SwitchState<DashState>();
    }
}

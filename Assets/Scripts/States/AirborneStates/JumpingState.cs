
using UnityEngine;

public class JumpingState : AirborneState
{
    private bool _canSwitchState;
    public JumpingState(Player player, IStateSwitcher stateSwitcher, AirborneStateConfig config) 
        : base(player, stateSwitcher, config)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Player.StatesData.TargetVelocity.y = Config.JumpForce;
        Player.Rigidbody.velocity = Player.StatesData.TargetVelocity;
        Player.StatesData.NumberOfAvailableJumps--;
        _canSwitchState = false;
    }

    public override void FixedUpdate()
    {
        if (!_canSwitchState)
        {
            _canSwitchState = true;
            return;
        }

        base.FixedUpdate();

        if (Player.Rigidbody.velocity.y < 0)
        {
            StateSwitcher.SwitchState<FallingState>();
        }

        Debug.Log(Player.Rigidbody.velocity);
    }
}

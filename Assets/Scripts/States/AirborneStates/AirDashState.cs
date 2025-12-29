using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AirDashState : AirborneState
{
    private Vector3 _previousVelocity;
    private float _previousMaxHorizontalSpeed;
    private DashStateConfig _config;
    private CancellationTokenSource _cts;

    public AirDashState(Player player, IStateSwitcher stateSwitcher, DashStateConfig dashConfig, AirborneStateConfig config)
        : base(player, stateSwitcher, config)
    {
        _config = dashConfig;
    }

    public override void Enter()
    {
        base.Enter();

        _cts = new CancellationTokenSource();
        _previousVelocity = Player.Rigidbody.velocity;
        _previousVelocity.y = 0;
        _previousMaxHorizontalSpeed = Player.StatesData.MaxHorizontalSpeed;
        Player.StatesData.SetDashReload(true, _config.ReloadTime + _config.Duration);

        Dash().Forget();
    }

    public override void Exit()
    {
        base.Exit();
        _cts.Cancel();
        Player.Rigidbody.velocity = _previousVelocity;
        Player.StatesData.MaxHorizontalSpeed = _previousMaxHorizontalSpeed;
    }

    private async UniTask Dash(CancellationToken token = default)
    {
        var direction = Player.Camera.transform.forward;

        Player.StatesData.MaxHorizontalSpeed = _config.Acceleration;
        Player.StatesData.Acceleration = _config.Acceleration;
        Player.StatesData.TargetVelocity = direction.normalized * Player.StatesData.Acceleration; 
        Player.Rigidbody.velocity = Player.StatesData.TargetVelocity;

        await UniTask.WaitForSeconds(_config.Duration, cancellationToken: token);

        StateSwitcher.SwitchState<FallingState>();

        Reload().Forget();
    }

    private async UniTask Reload()
    {
        await UniTask.WaitForSeconds(_config.ReloadTime);

        Player.StatesData.SetDashReload(false, _config.ReloadTime);
    }
}

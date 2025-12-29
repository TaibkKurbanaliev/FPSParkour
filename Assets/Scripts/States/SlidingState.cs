using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class SlidingState : GroundedState
{
    private SlidingStateConfig _config;
    private float _originalHeight;
    private CancellationTokenSource _cts;

    public SlidingState(Player player, IStateSwitcher stateSwitcher, GroundStateConfig groundConfig, SlidingStateConfig _slideConfig) 
        : base(player, stateSwitcher, groundConfig)
    {
        _config = _slideConfig;
    }

    public override void Enter()
    {
        base.Enter();

        _originalHeight = Player.Collider.height;

        Player.StatesData.IsSliding = true;
        Player.StatesData.TargetVelocity = Player.transform.forward * _config.Speed;
        Player.StatesData.TargetVelocity.y = 0f;
        Player.Collider.height = Player.Collider.radius * 2f + Config.CrouchHeight;
        Player.Collider.center = new Vector3(0f,
                                             -((_originalHeight - Player.Collider.height) / 2f),
                                             0f);

        _cts = new CancellationTokenSource();
        WaitSlide(_cts.Token).Forget();
    }

    public override void Exit()
    {
        base.Exit();
        Player.StatesData.IsSliding = false;
        Player.Collider.height = _originalHeight;
        Player.Collider.center = Vector3.zero;
        _cts.Cancel();
    }

    public override void Update()
    {
        base.Update();
    }

    protected override void Move()
    {
        Player.Rigidbody.velocity = Player.StatesData.TargetVelocity;

        if (Player.Input.Player.Move.ReadValue<Vector2>().y == 0f)
            Player.StatesData.TargetVelocity = Vector3.Lerp(Player.StatesData.TargetVelocity,
                                                            Vector3.zero,
                                                            _config.Deceleration * Time.fixedDeltaTime);
    }

    private async UniTask WaitSlide(CancellationToken token = default)
    {
        await UniTask.WaitForSeconds(_config.Duration, cancellationToken: token);

        if (Player.Checker.CheckHead())
            StateSwitcher.SwitchState<CrouchingState>();
        else
            StateSwitcher.SetPreviousState();
    }
}
